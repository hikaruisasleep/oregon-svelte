<script lang="ts">
	import EditableItemCard from '$lib/components/EditableItemCard.svelte';
	import type { PageData } from './$types';

	let { data }: { data: PageData } = $props();
</script>

<div class="products m-4 flex flex-col items-center">
	<div class="mb-4 flex w-full items-end justify-between">
		<h3 class="text-2xl">List produk</h3>

		<a href="/admin/items/add" class="text-sm text-white">
			<div class="rounded bg-green-600 px-3 py-1">
				<i class="fa-solid fa-plus"></i>
				Tambah produk baru
			</div>
		</a>
	</div>
	<div class="products-items flex w-full flex-col gap-2">
		{#each data.allProducts as product}
			<p>{product}</p>
		{/each}
	</div>
</div>
