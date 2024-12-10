<script lang="ts">
	import '../../../app.css';

	import type { Snippet } from 'svelte';
	import type { LayoutData } from './$types';
	import { goto } from '$app/navigation';

	let { data, children }: { data: LayoutData; children: Snippet } = $props();

	let isAdmin = data.currentUser.role == 1;

	let timer = $state(5);

	let countdown = setInterval(() => {
		timer--;
	}, 1000);

	function clearCountdownThenRedirect() {
		clearInterval(countdown);
		goto('/');
	}
</script>

<svelte:head>
	<title>Oregon Admin Panel</title>
</svelte:head>

{#if !isAdmin}
	<div class="flex h-svh w-svw flex-col items-center justify-center">
		<h1>You are not an admin!</h1>
		<p>Returning to homepage in {timer}</p>
		{#if timer == 0}{clearCountdownThenRedirect()}{/if}
	</div>
{:else}
	<div class="flex h-auto w-svw flex-col content-center items-center overflow-hidden">
		<div class="container m-0 flex h-full w-full flex-col border border-black p-0 md:w-[480px]">
			<header class="flex h-16 w-full flex-row items-center justify-between px-6 shadow-md">
				<a href="/admin">
					<h1 class="font-serif text-2xl leading-none md:text-3xl">0regon</h1>
					<p class="text-sm leading-none">Admin Panel</p>
				</a>
				<nav class="flex justify-between gap-2 md:gap-4">
					<a href="/">Return to storefront</a>
				</nav>
			</header>

			{@render children()}
		</div>
	</div>
{/if}
