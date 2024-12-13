<script lang="ts">
	import '../../app.css';

	import { type Snippet } from 'svelte';
	import type { LayoutData } from '../$types';
	let { children, data }: { children: Snippet; data: LayoutData } = $props();

	import mascot from '$lib/static/mascot.png';

	let navigationHidden = $state(true);

	let loggedIn = data.isLoggedIn;
</script>

<svelte:head>
	<title>0regon</title>
</svelte:head>

<div class="flex h-auto min-h-svh w-svw flex-col content-center items-center overflow-visible">
	<div class="m-0 flex h-full w-full flex-col p-0 md:w-[480px]">
		<header
			class="sticky top-0 z-10 flex h-16 w-full flex-row items-center justify-between bg-white px-6 shadow-md"
		>
			<a href="/" class="flex h-[150%] -translate-x-8 flex-row items-center justify-start">
				<img src={mascot} alt="mascot" class="h-full translate-y-2" />
				<h1 class="-translate-x-4 font-serif text-2xl text-violet-700 md:text-3xl">0regon</h1>
			</a>
			<div class:hidden={!loggedIn} class="flex flex-row justify-end gap-3">
				<a href="/cart" aria-label="cart"
					><i class="fa-solid fa-cart-shopping fa-xl leading-normal"></i></a
				>
				<button
					aria-label="expand navigation"
					onclick={() => {
						navigationHidden = false;
					}}
				>
					<i class="fa-solid fa-bars fa-xl leading-normal"></i>
				</button>
				<div
					class="fixed left-0 top-0 z-10 flex w-full flex-shrink flex-grow-0 flex-col items-end justify-center overflow-x-hidden bg-gray-300 transition-all duration-500 md:left-1/2 md:w-[480px] md:-translate-x-1/2"
					class:h-0={navigationHidden}
					class:h-full={!navigationHidden}
				>
					<button
						aria-label="close navigation"
						class="relative flex h-[5%] flex-none flex-col items-end"
						onclick={() => {
							navigationHidden = true;
						}}
					>
						<i class="fa-solid fa-xmark fa-2xl mx-16 leading-normal"></i>
					</button>
					<nav
						class="relative flex h-[65%] w-full flex-col items-center justify-center gap-12 text-2xl md:gap-16"
					>
						<p class="text-xs">Currently logged in as {data.currentUser.name}</p>
						<a
							href="/"
							onclick={() => {
								navigationHidden = true;
							}}
						>
							Home
						</a>
						<a
							href="/profile"
							onclick={() => {
								navigationHidden = true;
							}}
						>
							Profile
						</a>
						{#if data.isAdmin}
							<a
								href="/admin"
								onclick={() => {
									navigationHidden = true;
								}}
							>
								Admin Panel
							</a>
						{/if}
					</nav>
					<div class="flex h-[5%] w-full flex-col items-center">
						<form method="post" action="/?/logout">
							<button class="rounded-lg bg-red-800 px-3 py-2 text-white" type="submit">
								Log out
							</button>
						</form>
					</div>
				</div>
			</div>
			<div class:hidden={loggedIn} class="w-max">
				<a href="/login" class="w-max text-nowrap">Log in</a>
			</div>
		</header>

		{@render children()}
	</div>
</div>
