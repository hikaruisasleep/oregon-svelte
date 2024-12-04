using System.Diagnostics;
using System.Numerics;
using ImGuiNET;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using oregon_backend.Models;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using oregon_backend.types;

namespace oregon_backend;

class Program
{
    private static Sdl2Window _window;
    private static GraphicsDevice _gd;
    private static ImGuiController _controller;
    private static CommandList _cl;
    
    private static String _connString = "";
    private static Vector3 _clearColor = new(0.45f, 0.55f, 0.6f);

    public static string db = "";
    public static string host = "";
    public static string port = "";
    public static string user = "";
    public static string password = "";
    private static bool trustServer = false;

    private static IHost? serverHost;
    private static Thread? serverThread;

    /*
     * dotnet ef migrations add migration_name -- --migrate
     * dotnet ef database update -- --migrate
     */
    private static void Main(string[] args)
    {
        InitConfig();
        if (args.Contains("migrate"))
        {
            CreateHostBuilder(args).Build().Run();
            return;
        }
        
        VeldridStartup.CreateWindowAndGraphicsDevice(
            new WindowCreateInfo(50, 50, 800, 400, WindowState.Normal, "Oregon - Backend"),
            new GraphicsDeviceOptions(true, null, true, ResourceBindingModel.Improved, true, true),
            out _window,
            out _gd);
        
        _window.Resized += () =>
        {
            _gd.MainSwapchain.Resize((uint)_window.Width, (uint)_window.Height);
            _controller.WindowResized(_window.Width, _window.Height);
        };
        
        _window.Closed += () =>
        {
            if (serverHost != null)
            {
                serverHost.StopAsync().Wait();
            }
            if (serverThread != null)
            {
                serverThread.Join();
            }
        };

        _cl = _gd.ResourceFactory.CreateCommandList();
        _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);

        var stopwatch = Stopwatch.StartNew();
        var deltaTime = 0f;
        
        while (_window.Exists)
        {
            ImGui.SetNextWindowPos(new Vector2(0, 0));
            deltaTime = stopwatch.ElapsedTicks / (float)Stopwatch.Frequency;
            stopwatch.Restart();
            var snapshot = _window.PumpEvents();
            if (!_window.Exists) { break; }
            _controller.Update(deltaTime, snapshot);

            ImGui.SetNextWindowPos(Vector2.Zero, ImGuiCond.Always);
            ImGui.SetNextWindowSize(new Vector2(_window.Width, _window.Height), ImGuiCond.Always);
            var windowFlags = ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar;

            ImGui.Begin("Oregon", windowFlags); 
            RenderUI();
            
            _cl.Begin();
            _cl.SetFramebuffer(_gd.MainSwapchain.Framebuffer);
            
            _cl.ClearColorTarget(0, new RgbaFloat(_clearColor.X, _clearColor.Y, _clearColor.Z, 1f));
            
            _controller.Render(_gd, _cl);

            _cl.End();
            _gd.SubmitCommands(_cl);
            _gd.SwapBuffers(_gd.MainSwapchain);
        }

        _gd.WaitForIdle();
        _controller.Dispose();
        _cl.Dispose();
        _gd.Dispose();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://localhost:5000");
                webBuilder.ConfigureServices(services =>
                {
                    services.AddControllers();
                    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_connString));
                    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "Oregon",
                            ValidAudience = "Oregon",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("659S0HjRMMI5Mr8YoKOAiTmWDvk6xgKW"))
                        };
                    });
                });
                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseMiddleware<JwtMiddleware>();
                    app.UseAuthentication();
                    app.UseAuthorization();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
        return host;
    }
    
    private static unsafe void RenderUI()
    {
        ImGui.SeparatorText("Controls");
        ImGui.InputText("Database", ref db, 100);
        ImGui.InputText("User", ref user, 100);
        ImGui.InputText("Password", ref password, 100);
        ImGui.InputText("Host", ref host, 100);
        ImGui.Checkbox("Trust Server Certificate", ref trustServer);
        if (ImGui.Button("Connect"))
        {
            SaveConfig();
            _connString = $"Server={host};Database={db};User={user};Password={password};TrustServerCertificate={trustServer};";
            serverHost = CreateHostBuilder(null).Build();
            serverThread= new Thread(() =>
            {
                serverHost.Run();
            });
            serverThread.Start();
        }
    }

    private static void SaveConfig()
    { 
        var config = new Config()
        {
            Database = new types.Database()
            {
                Db = db,
                Host = host,
                Port = port,
                User = user,
                Password = password,
                TrustServerCertificate = trustServer
            },
            JwtSecret = ""
        };

        var jsonString = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("config.json", jsonString);
    }

    private static void InitConfig()
    {
        if (!File.Exists("config.json"))
        {
            var config = new Config()
            {
                Database = new types.Database()
                {
                    Db = "",
                    Host = "",
                    Port = "",
                    User = "",
                    Password = "",
                    TrustServerCertificate = false
                },
                JwtSecret = ""
            };
            var jsonString = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("config.json", jsonString);
        }
        else
        {
            var json = File.ReadAllText("config.json");
            var config = JsonSerializer.Deserialize<Config>(json);
            _connString = $"Server={config.Database.Host};Database={config.Database.Db};User={config.Database.User};Password={config.Database.Password};TrustServerCertificate={config.Database.TrustServerCertificate};";
            db = config.Database.Db;
            host = config.Database.Host;
            port = config.Database.Port;
            user = config.Database.User;
            password = config.Database.Password;
            trustServer = config.Database.TrustServerCertificate;
        }
    }
}
