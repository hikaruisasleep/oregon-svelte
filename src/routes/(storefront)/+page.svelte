<script lang="ts">
	import ItemCard from '$lib/components/ItemCard.svelte';
	import FeaturedItemCard from '$lib/components/FeaturedItemCard.svelte';
	import CategoryCard from '$lib/components/CategoryCard.svelte';

	import type { LayoutData } from './$types';

	let { data }: { data: LayoutData } = $props();
</script>

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

<div class="featured m-4">
	<h3 class="mb-4 text-[0]">
		<span class="text-xl font-normal">produk</span>
		<span class="text-xl font-thin">unggulan</span>
	</h3>
	<div class="recommended-items flex flex-row gap-2 overflow-scroll">
		<FeaturedItemCard />
	</div>
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
