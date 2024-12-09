<script>
	let props = $props();
	let product = props.product;
</script>

<div
	class="grid h-24 w-full flex-shrink-0 flex-grow-0 grid-cols-4 grid-rows-1 rounded border border-black"
>
	{#if product.imageUrl != ''}
		<div class="col-span-1 bg-cover" style="background-image: url({product.imageUrl});"></div>
	{:else}
		<div class="col-span-1 bg-violet-400"></div>
	{/if}
	<div class="col-span-2 flex flex-col justify-between px-3 py-3 shadow-inner">
		<div>
			<p class="font-light">{product.name}</p>
			<p class="font-medium text-green-800">
				Rp{product.price.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, '.')}
			</p>
		</div>
	</div>
</div>
