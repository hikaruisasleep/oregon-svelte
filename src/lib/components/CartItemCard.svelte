<script>
	import { enhance } from '$app/forms';

	let { cartItem } = $props();
	let product = cartItem.product;
	let productQty = $state(cartItem.quantity);

	let qtyForm = $state();

	/**
	 * @param {string} action
	 * @param {number} quantity
	 */
	async function updateQuantity(action, quantity) {
		if (action == 'add') {
			quantity++;
		} else if (action == 'subtract' && quantity > 0) {
			quantity--;
		}

		return quantity;
	}
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

	<div class="flex w-fit items-center justify-center">
		<form
			action="?/updatecart"
			method="post"
			bind:this={qtyForm}
			class="flex h-7 w-fit flex-row items-center justify-center overflow-clip rounded-lg border border-gray-800"
			id={cartItem.id}
			use:enhance
		>
			<button
				type="button"
				class="w-6 bg-gray-200"
				onclick={async () => {
					productQty > 0 ? productQty-- : (productQty = 0);
					await new Promise((r) => setTimeout(r, 1));
					qtyForm.submit();
				}}
			>
				-
			</button>
			<input type="hidden" name="product-id" value={cartItem.productId} />
			<input type="hidden" name="cart-id" value={cartItem.id} />
			<input
				type="number"
				name={`qty-${cartItem.productId}`}
				id={`qty-${cartItem.productId}`}
				bind:value={productQty}
				class="w-8 border border-y-0 border-gray-800 bg-white p-0 text-center"
			/>
			<button
				type="button"
				class="w-6 bg-gray-200"
				onclick={async () => {
					productQty++;
					await new Promise((r) => setTimeout(r, 1));
					qtyForm.submit();
				}}
			>
				+
			</button>
		</form>
	</div>
</div>
