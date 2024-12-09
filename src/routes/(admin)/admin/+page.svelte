<script lang="ts">
	import UneditableItemCard from '$lib/components/UneditableItemCard.svelte';
	import type { LayoutData } from './$types';

	let { data }: { data: LayoutData } = $props();
</script>

<div class="products m-4 flex flex-col items-center">
	<div class="mb-4 flex w-full items-end justify-between">
		<h3 class="text-2xl">List produk</h3>
		<a href="/admin/items" class="text-sm text-violet-800"> Lihat semua </a>
	</div>
	<div class="products-items flex h-60 w-full flex-col gap-2 overflow-scroll">
		{#each data.allProducts as product}
			<UneditableItemCard {product} />
		{/each}
	</div>
</div>
