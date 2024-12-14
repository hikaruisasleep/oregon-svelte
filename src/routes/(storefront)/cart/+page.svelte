<script lang="ts">
	import type { PageData } from './$types';
	import CartItemCard from '$lib/components/CartItemCard.svelte';

	let { data }: { data: PageData } = $props();

	let totalPrice = $derived.by(() => {
		let temp = 0;
		for (const item of data.userCart) {
			temp += parseInt(item.product.price) * parseInt(item.quantity);
		}
		return temp;
	});

	let checkoutAnimation = $state(false);
</script>

<h3 class="m-4 mt-6 text-[0]">
	<span class="inline-block -translate-y-1/2 text-xs"><i class="fa-solid fa-sparkle"></i></span>
	<span class="text-xl font-normal">keranjang</span>
	<span class="text-xl font-thin">belanjamu</span>
</h3>

<div class="flex flex-col gap-4">
	{#each data.userCart as cartItem (cartItem.productId)}
		<CartItemCard {cartItem} />
	{/each}
</div>

<div
	class="fixed bottom-0 flex h-16 w-full flex-row items-center justify-between gap-1 bg-violet-400 px-8 text-white md:w-[480px]"
>
	<div>
		<p class="text-sm font-extralight">Total</p>
		<p class="text-lg font-medium">
			Rp{totalPrice.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, '.')}
		</p>
	</div>
	<form
		class="relative before:absolute before:bottom-0 before:right-0 before:-z-10 before:inline-block before:h-12 before:w-12 before:translate-y-1 before:rounded-full before:bg-purple-900"
		action="?/checkout"
		method="POST"
	>
		<button
			class="h-12 w-12 rounded-full bg-white transition-all duration-500 ease-out"
			aria-label="checkout"
			onclick={() => {
				checkoutAnimation = true;
			}}
			class:translate-y-1={checkoutAnimation}
		>
			<i class="fa-solid fa-cart-circle-check font-xl text-violet-800"></i>
		</button>
	</form>
</div>
