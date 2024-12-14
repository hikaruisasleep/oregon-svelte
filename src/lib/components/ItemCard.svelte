<script>
	let { product } = $props();
</script>

<a href="/product/{product.id}">
	<div
		class="grid h-72 w-36 flex-shrink-0 flex-grow-0 grid-cols-1 grid-rows-7 rounded border border-black"
	>
		{#if product.imageUrl != ''}
			<div
				class="row-span-4 bg-cover bg-center"
				style="background-image: url({product.imageUrl});"
			></div>
		{:else}
			<div class="row-span-4 bg-violet-400"></div>
		{/if}
		<div class="row-span-3 flex flex-col justify-between px-3 py-3 shadow-inner">
			<div>
				<p class="truncate font-light">{product.name}</p>
				<p class="font-medium text-indigo-600">
					Rp{product.price.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, '.')}
				</p>
			</div>
			<div>
				<p class="text-right">
					4.8/5 <i class="fa-solid fa-star text-orange-400 drop-shadow-sm"></i>
				</p>
			</div>
		</div>
	</div>
</a>
