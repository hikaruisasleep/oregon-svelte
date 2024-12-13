<script lang="ts">
	import type { PageData } from './$types';

	import ItemCard from '$lib/components/ItemCard.svelte';
	import FeaturedItemCard from '$lib/components/FeaturedItemCard.svelte';

	import ctaMascot from '$lib/static/cta-mascot.png';

	import randomizeArray from '$lib/randomizeArray';

	let { data }: { data: PageData } = $props();
	let allProducts = [...data.allProducts];

	let searchForm: HTMLFormElement = $state();
</script>

<div class="featured flex h-60 w-full flex-col items-stretch bg-purple-800">
	<div
		class="featured-items flex w-full snap-x snap-mandatory flex-row gap-6 overflow-x-auto py-4 before:w-32 before:shrink-0 after:w-60 after:shrink-0"
	>
		{#each allProducts as product}
			<FeaturedItemCard {product} />
		{/each}
	</div>
</div>

<div
	class="mx-auto flex w-4/5 -translate-y-1/4 flex-col gap-4 rounded-3xl bg-pink-300 p-6 md:-translate-y-1/3"
>
	<h3 class="w-2/3 text-2xl font-semibold sm:w-1/2 md:w-2/3">
		Hidup cuma sekali, kenapa nggak untuk yang kamu cintai?
	</h3>
	<div
		class=" flex flex-row items-center justify-around gap-1 rounded-full border-2 border-black bg-white px-1"
	>
		<button
			type="button"
			onclick={() => {
				searchForm.submit();
			}}
			aria-label="search"
		>
			<i class="fa-solid fa-magnifying-glass fa-xl leading-normal"></i>
		</button>
		<form action="/search" method="GET" class="h-11/12 w-11/12 md:w-5/6" bind:this={searchForm}>
			<input
				type="search"
				name="term"
				id="searchbar"
				class="m-0 h-full w-full border-none p-0 font-thin"
				placeholder="Mau khilaf apa hari ini?"
			/>
		</form>
	</div>
	<div
		class="pointer-events-none fixed -bottom-16 -right-16 flex h-60 w-36 items-center justify-center overflow-hidden"
	>
		<img src={ctaMascot} alt="cat" class="pointer-events-none h-60 w-72 max-w-[unset]" />
	</div>
</div>

<div class="recommended m-4">
	<h3 class="mb-4 text-[0]">
		<span class="inline-block -translate-y-1/2 text-xs"><i class="fa-solid fa-sparkle"></i></span>
		<span class="text-xl font-normal">rekomendasi</span>
		<span class="text-xl font-thin">untukmu</span>
	</h3>
	<div class="recommended-items flex flex-row gap-2 overflow-scroll">
		{#each randomizeArray(allProducts) as product}
			<ItemCard {product} />
		{/each}
	</div>
</div>

<div class="popular m-4">
	<h3 class="mb-4 text-[0]">
		<span class="inline-block -translate-y-1/2 text-xs"><i class="fa-solid fa-sparkle"></i></span>
		<span class="text-xl font-normal">produk</span>
		<span class="text-xl font-thin">terpopuler</span>
	</h3>
	<div class="popular-items flex flex-row gap-2 overflow-scroll">
		{#each allProducts.sort((a, b) => b.pageView - a.pageView) as product}
			<ItemCard {product} />
		{/each}
	</div>
</div>

<style lang="scss">
	.featured *::-webkit-scrollbar-thumb {
		display: none;
	}

	.featured * {
		-ms-overflow-style: none;
		scrollbar-width: none;
	}
</style>
