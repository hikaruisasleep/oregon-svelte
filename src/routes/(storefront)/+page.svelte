<script lang="ts">
	import ItemCard from '$lib/components/ItemCard.svelte';
	import CategoryCard from '$lib/components/CategoryCard.svelte';
	import LocationCard from '$lib/components/LocationCard.svelte';
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

<div class="recommended m-4">
	<h3 class="mb-4 text-xl">Rekomendasi untukmu</h3>
	<div class="recommended-items flex flex-row gap-2 overflow-scroll">
		<ItemCard></ItemCard>
		<ItemCard></ItemCard>
		<ItemCard></ItemCard>
		<ItemCard></ItemCard>
		<ItemCard></ItemCard>
		<ItemCard></ItemCard>
		<ItemCard></ItemCard>
	</div>
</div>

<div class="categories m-4">
	<h3 class="mb-4 text-xl">Kategori terpopuler</h3>
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

<div class="location m-4">
	<h3 class="mb-4 text-xl">Cari berdasarkan lokasi</h3>
	<div class="location-items flex flex-row gap-2 overflow-scroll">
		<LocationCard></LocationCard>
		<LocationCard></LocationCard>
		<LocationCard></LocationCard>
		<LocationCard></LocationCard>
		<LocationCard></LocationCard>
	</div>
</div>
