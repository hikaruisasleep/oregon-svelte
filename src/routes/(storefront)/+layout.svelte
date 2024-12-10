<script lang="ts">
	import '../../app.css';

	import { getContext, setContext, type Snippet } from 'svelte';
	import type { LayoutData } from '../$types';
	let { children, data }: { children: Snippet; data: LayoutData } = $props();

	let navigationHidden = $state(true);

	if (data.session_token == undefined) {
		setContext('logged_in', false);
	} else {
		setContext('logged_in', true);
	}

	let loggedIn = getContext('logged_in');
</script>

<svelte:head>
	<title>0regon</title>
</svelte:head>

<div class="flex h-auto w-svw flex-col content-center items-center overflow-visible">
	<div class="m-0 flex h-full w-full flex-col p-0 md:w-[480px]">
		<header
			class="sticky top-0 z-10 flex h-16 w-full flex-row items-center justify-between bg-white px-6 shadow-md"
		>
			<a href="/"><h1 class="font-serif text-2xl text-violet-700 md:text-3xl">0regon</h1></a>
			<div class:hidden={!loggedIn}>
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
						<a
							href="/"
							onclick={() => {
								navigationHidden = true;
							}}
						>
							Home
						</a>
						<a
							href="/categories"
							onclick={() => {
								navigationHidden = true;
							}}
						>
							Categories
						</a>
						<a
							href="/profile"
							onclick={() => {
								navigationHidden = true;
							}}
						>
							Profile
						</a>
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
			<div class:hidden={loggedIn}>
				<a href="/login">Log in</a>
			</div>
		</header>

		{@render children()}
	</div>
</div>
