<script>
	let { product } = $props();
</script>

<a href="/product/{product.id}">
	<div
		class="h-32 w-60 flex-shrink-0 flex-grow-0 snap-center grid-cols-1 grid-rows-7 rounded border border-black bg-violet-400"
	>
		{#if product.imageUrl != ''}
			<div class="row-span-4 bg-cover" style="background-image: url({product.imageUrl});"></div>
		{:else}
			<div class="row-span-4 bg-violet-400"></div>
		{/if}
	</div>
</a>
