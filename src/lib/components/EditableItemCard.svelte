<script>
	let { product } = $props();
</script>

<div
	class="grid h-24 w-full flex-shrink-0 flex-grow-0 grid-cols-4 grid-rows-1 rounded border border-black"
>
	{#if product.imageUrl != ''}
		<div
			class="col-span-1 bg-cover bg-center"
			style="background-image: url({product.imageUrl});"
		></div>
	{:else}
		<div class="col-span-1 bg-violet-400"></div>
	{/if}
	<div class="col-span-2 flex flex-col justify-between px-3 py-3 shadow-inner">
		<div class="h-full overflow-clip">
			<p class="truncate font-light">{product.name}</p>
			<p class="font-medium text-green-800">
				Rp{product.price.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, '.')}
			</p>
		</div>
	</div>
	<div class="col-span-1 flex flex-col justify-around px-5 py-3 text-right">
		<a href="/admin/items/edit/{product.id}" class="text-blue-700">
			<i class="fa-solid fa-pen-to-square fa-lg leading-normal"></i> Edit
		</a>
	</div>
</div>
