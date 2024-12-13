<script lang="ts">
	import { enhance } from '$app/forms';
	import type { ActionData } from './$types';

	let { form }: { form: ActionData } = $props();

	import mascot from '$lib/static/mascot.png';

	let formLoading = $state(false);
</script>

<header class="flex h-16 w-full flex-row items-center justify-between px-6 shadow-md">
	<a href="/" class="flex h-[150%] -translate-x-8 flex-row items-center justify-start">
		<img src={mascot} alt="mascot" class="h-full translate-y-2" />
		<h1 class="-translate-x-4 font-serif text-2xl text-violet-700 md:text-3xl">0regon</h1>
	</a>
	<div>
		<a href="/register">Register</a>
	</div>
</header>

<div class="flex h-svh flex-col justify-center gap-12 px-4 pb-16">
	<div>
		<h1>Login ke 0regon</h1>
		{#if form?.status == 'Success'}
			<h3>Berhasil login!</h3>
		{:else if form?.errored}
			<p class="text-sm text-red-950">Error: {form.reason}</p>
		{/if}
	</div>
	<form
		method="post"
		class="flex flex-col justify-center gap-4"
		action="?/login"
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
			<label for="password">Password</label>
			<input type="password" name="password" id="password" />
		</div>

		{#if formLoading}
			<button
				type="submit"
				class="flex h-8 w-24 flex-row items-center justify-evenly self-center rounded-xl bg-violet-400 font-semibold text-white"
			>
				<i class="fa-solid fa-spinner fa-xl animate-spin leading-normal"></i>
				Login
			</button>
		{:else}
			<button
				type="submit"
				class="h-8 w-fit self-center rounded-xl bg-violet-400 px-6 font-semibold text-white"
			>
				Login
			</button>
		{/if}
	</form>

	<p>Belum punya akun? <a href="/register" class="text-violet-700">Register!</a></p>
</div>
