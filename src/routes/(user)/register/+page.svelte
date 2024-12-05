<header class="flex h-16 w-full flex-row items-center justify-between px-6 shadow-md">
	<a href="/"><h1 class="font-serif text-2xl text-violet-700 md:text-3xl">0regon</h1></a>
	<div>
		<a href="/login">Login</a>
	</div>
</header>

<div class="flex h-svh flex-col justify-center gap-12 px-4 pb-16">
	<h1>Registrasi akun baru</h1>
	<form action="" method="post" class="flex flex-col justify-center gap-4">
		<div class="flex flex-col">
			<label for="email">Email</label>
			<input type="email" name="email" id="email" />
		</div>
		<div class="flex flex-col">
			<label for="name">Full Name</label>
			<input type="text" name="name" id="name" />
		</div>
		<div class="flex flex-col">
			<label for="username">Username</label>
			<input type="text" name="username" id="username" />
		</div>
		<div class="flex flex-col">
			<label for="password">Password</label>
			<input type="password" name="password" id="password" />
		</div>
		<input
			type="submit"
			value="Register"
			class="w-fit self-center rounded-xl bg-violet-400 px-8 font-semibold text-white"
		/>
	</form>

	<p>Sudah punya akun? <a href="/login" class="text-violet-700">Login!</a></p>
</div>
