using System.Diagnostics;
using System.Numerics;
using ImGuiNET;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace oregon_backend;

class Program
{
    private static Sdl2Window _window;
    private static GraphicsDevice _gd;
    private static ImGuiController _controller;
    private static CommandList _cl;

    private static Vector3 _clearColor = new(0.45f, 0.55f, 0.6f);

    private static void Main(string[] args)
    {
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

        _cl = _gd.ResourceFactory.CreateCommandList();
        _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);

        var stopwatch = Stopwatch.StartNew();
        var deltaTime = 0f;
        
        var serverThread = new Thread(RunServer);
        serverThread.Start();
        
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

    private static void RunServer()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://localhost:5000");
                webBuilder.ConfigureServices(services =>
                {
                    services.AddControllers();
                });
                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            })
            .Build();

        host.Run();
    }

    private static unsafe void RenderUI()
    {
        ImGui.Text("Oregon - Admin");
    }
}
