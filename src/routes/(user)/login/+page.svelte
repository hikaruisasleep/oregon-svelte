<script lang="ts">
	import { redirect } from '@sveltejs/kit';
	import type { ActionData, PageData } from './$types';

	let { form, load }: { form: ActionData; load: PageData } = $props();
</script>

<header class="flex h-16 w-full flex-row items-center justify-between px-6 shadow-md">
	<a href="/"><h1 class="font-serif text-2xl text-violet-700 md:text-3xl">0regon</h1></a>
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
	<form method="post" class="flex flex-col justify-center gap-4" action="?/login">
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

	<p>Belum punya akun? <a href="/register" class="text-violet-700">Register!</a></p>
</div>
