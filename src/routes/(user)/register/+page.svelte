<script>
	import { enhance } from '$app/forms';
	import mascot from '$lib/static/mascot.png';

	let formLoading = $state(false);
</script>

<header class="flex h-16 w-full flex-row items-center justify-between px-6 shadow-md">
	<a href="/" class="flex h-[150%] -translate-x-8 flex-row items-center justify-start">
		<img src={mascot} alt="mascot" class="h-full translate-y-2" />
		<h1 class="-translate-x-4 font-serif text-2xl text-violet-700 md:text-3xl">0regon</h1>
	</a>
	<div>
		<a href="/login">Login</a>
	</div>
</header>

<div class="flex h-svh flex-col justify-center gap-12 px-4 pb-16">
	<h1>Registrasi akun baru</h1>
	<form
		action=""
		method="post"
		class="flex flex-col justify-center gap-4"
		use:enhance={() => {
			formLoading = true;
			return async ({ update }) => {
				formLoading = false;
				update();
			};
		}}
	>
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

		{#if formLoading}
			<button
				type="submit"
				class="flex h-8 w-24 flex-shrink-0 flex-row items-center justify-evenly self-center rounded-xl bg-violet-400 font-semibold text-white"
			>
				<i class="fa-solid fa-spinner fa-sm animate-spin leading-normal"></i>
				Register
			</button>
		{:else}
			<button
				type="submit"
				class="h-8 w-fit self-center rounded-xl bg-violet-400 px-6 font-semibold text-white"
			>
				Register
			</button>
		{/if}
	</form>

	<p>Sudah punya akun? <a href="/login" class="text-violet-700">Login!</a></p>
</div>
