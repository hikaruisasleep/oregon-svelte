<script lang="ts">
	import UneditableItemCard from '$lib/components/UneditableItemCard.svelte';
	import UneditableUserCard from '$lib/components/UneditableUserCard.svelte';
	import type { LayoutData } from './$types';

	let { data }: { data: LayoutData } = $props();
</script>

<div class="products m-4 flex flex-col items-center">
	<div class="mb-4 flex w-full items-end justify-between">
		<h3 class="text-2xl">List produk</h3>
		<a href="/admin/items" class="text-sm text-violet-800"> Lihat semua </a>
	</div>
	<div class="products-items flex w-full flex-col gap-2 overflow-hidden h-96">
		{#each data.allProducts as product}
			<UneditableItemCard {product} />
		{/each}
	</div>
</div>

<div class="users m-4 flex flex-col items-center">
	<div class="mb-4 flex w-full items-end justify-between">
		<h3 class="text-2xl">List user</h3>
		<a href="/admin/items" class="text-sm text-violet-800"> Lihat semua </a>
	</div>
	<div class="products-items flex w-full flex-col gap-2 overflow-hidden h-80">
		{#each data.allUsers as user}
			<UneditableUserCard {user} />
		{/each}
	</div>
</div>
