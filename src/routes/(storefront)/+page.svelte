<script lang="ts">
	import ItemCard from '$lib/components/ItemCard.svelte';
	import FeaturedItemCard from '$lib/components/FeaturedItemCard.svelte';
	import CategoryCard from '$lib/components/CategoryCard.svelte';

	import type { LayoutData } from './$types';

	let { data }: { data: LayoutData } = $props();

	let numberOfCarouselItems = $state(5);
</script>

<div class="featured flex h-60 w-full flex-col items-stretch bg-purple-800">
	<div
		class="featured-items flex w-full snap-x snap-mandatory flex-row gap-6 overflow-x-auto py-4 before:w-32 before:shrink-0 after:w-60 after:shrink-0"
	>
		{#each data.allProducts as product}
			<FeaturedItemCard {product} />
		{/each}
	</div>
</div>

<div
	class="mx-4 mb-0 mt-4 flex flex-row items-center justify-around gap-1 rounded-lg border-2 border-black px-1"
>
	<i class="fa-solid fa-magnifying-glass fa-xl leading-normal"></i>
	<input
		type="search"
		name="search"
		id="searchbar"
		class="m-0 h-5/6 w-11/12 border-none p-0"
		placeholder="Cari di website ini..."
	/>
</div>

<div class="recommended m-4">
	<h3 class="mb-4 text-[0]">
		<span class="text-xl font-normal">rekomendasi</span>
		<span class="text-xl font-thin">untukmu</span>
	</h3>
	<div class="recommended-items flex flex-row gap-2 overflow-scroll">
		{#each data.allProducts as product}
			<ItemCard {product} />
		{/each}
	</div>
</div>

<div class="categories m-4">
	<h3 class="mb-4 text-[0]">
		<span class="text-xl font-normal">kategori</span>
		<span class="text-xl font-thin">terpopuler</span>
	</h3>
	<div class="category-items grid grid-flow-col grid-rows-2 gap-2 overflow-scroll">
		<CategoryCard></CategoryCard>
		<CategoryCard></CategoryCard>
		<CategoryCard></CategoryCard>
		<CategoryCard></CategoryCard>
		<CategoryCard></CategoryCard>
		<CategoryCard></CategoryCard>
		<CategoryCard></CategoryCard>
		<CategoryCard></CategoryCard>
		<CategoryCard></CategoryCard>
		<CategoryCard></CategoryCard>
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
