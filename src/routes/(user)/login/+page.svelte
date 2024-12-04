<header class="flex h-16 w-full flex-row items-center justify-between px-6 shadow-md">
	<a href="/"><h1 class="font-serif text-2xl text-violet-700 md:text-3xl">0regon</h1></a>
	<div>
		<a href="/register">Register</a>
	</div>
</header>

<div class="flex h-svh flex-col justify-center gap-12 px-4 pb-16">
	<h1>Login to 0regon</h1>
	<form method="post" class="flex flex-col justify-center gap-4">
		<div class="flex flex-col">
			<label for="email">Email</label>
			<input type="email" name="email" id="email" />
		</div>
		<div class="flex flex-col">
			<label for="password">Password</label>
			<input type="password" name="password" id="password" />
		</div>
		<input
			type="submit"
			value="Login"
			class="w-fit self-center rounded-xl bg-violet-400 px-10 font-semibold text-white"
		/>
	</form>
</div>
