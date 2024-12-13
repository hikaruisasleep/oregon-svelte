<script lang="ts">
	import ItemCard from '$lib/components/ItemCard.svelte';
	import FeaturedItemCard from '$lib/components/FeaturedItemCard.svelte';
	import CategoryCard from '$lib/components/CategoryCard.svelte';

	import ctaMascot from '$lib/static/cta-mascot.png';

	import type { LayoutData } from './$types';

	let { data }: { data: LayoutData } = $props();

	let numberOfCarouselItems = $state(5);

	let allProducts = [...data.allProducts];
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
	<h3 class="w-1/2 text-2xl font-semibold">
		Hidup cuma sekali, kenapa nggak untuk yang kamu cintai?
	</h3>
	<div
		class=" flex flex-row items-center justify-around gap-1 rounded-full border-2 border-black bg-white px-1"
	>
		<i class="fa-solid fa-magnifying-glass fa-xl leading-normal"></i>
		<input
			type="search"
			name="search"
			id="searchbar"
			class="h-11/12 m-0 w-5/6 border-none p-0 font-thin sm:w-11/12"
			placeholder="Mau khilaf apa hari ini?"
		/>
	</div>
	<div
		class="fixed -bottom-16 -right-16 flex h-60 w-36 items-center justify-center overflow-hidden"
	>
		<img src={ctaMascot} alt="cat" class="h-60 w-72 max-w-[unset]" />
	</div>
</div>

<div class="recommended m-4">
	<h3 class="mb-4 text-[0]">
		<span class="text-xl font-normal">rekomendasi</span>
		<span class="text-xl font-thin">untukmu</span>
	</h3>
	<div class="recommended-items flex flex-row gap-2 overflow-scroll">
		{#each allProducts as product}
			<ItemCard {product} />
		{/each}
	</div>
</div>

<div class="popular m-4">
	<h3 class="mb-4 text-[0]">
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
