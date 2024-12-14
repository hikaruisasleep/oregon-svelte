<script lang="ts">
	import ItemCard from '$lib/components/ItemCard.svelte';
	import type { PageData } from './$types';

	let { data }: { data: PageData } = $props();

	let searchTerm = $state();
	let searchForm = $state();

	if (data.searchTerm) {
		searchTerm = data.searchTerm;
	}
</script>

<div class="searchbar-container m-4">
	<h3 class="mb-4 text-[0]">
		<span class="inline-block -translate-y-1/2 text-xs"><i class="fa-solid fa-sparkle"></i></span>
		<span class="text-xl font-normal">cari</span>
		<span class="text-xl font-thin">produk</span>
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
				placeholder="Cari di website ini..."
				bind:value={searchTerm}
			/>
		</form>
	</div>
</div>

{#if data.searchResult.length >= 1}
	<div class="searchresult m-4 flex h-fit min-h-[75vh] flex-col items-start justify-start">
		<h3 class="mb-4 text-[0]">
			<span class="inline-block -translate-y-1/2 text-xs"><i class="fa-solid fa-sparkle"></i></span>
			<span class="text-xl font-normal">hasil</span>
			<span class="text-xl font-thin">pencarian</span>
		</h3>
		<div class="searchresult-items grid h-auto w-full grid-flow-col gap-2">
			{#each data.searchResult as product}
				<ItemCard {product} />
			{/each}
		</div>
	</div>
{:else}
	<div class="searchresult m-4 flex h-fit min-h-[76svh] flex-col items-center justify-center">
		<h3 class="h-fit w-fit text-center text-lg">
			<span class="inline-block -translate-y-1/2 text-xs"><i class="fa-solid fa-sparkle"></i></span>
			<span class="font-thin">maaf, kami tidak menemukan hasil untuk</span>
			<br />
			<span class="font-normal">{data.searchTerm}</span>
		</h3>
	</div>
{/if}
