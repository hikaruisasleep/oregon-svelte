<script lang="ts">
	import type { PageData } from './$types';
	import CartItemCard from '$lib/components/CartItemCard.svelte';

	let { data }: { data: PageData } = $props();

	let totalPrice = $state(0);

	$effect(() => {
		totalPrice = 0;
		for (const item of data.userCart) {
			totalPrice += parseInt(item.product.price) * parseInt(item.quantity);
		}
	});
</script>

<h3 class="m-4 mt-6 text-[0]">
	<span class="inline-block -translate-y-1/2 text-xs"><i class="fa-solid fa-sparkle"></i></span>
	<span class="text-xl font-normal">keranjang</span>
	<span class="text-xl font-thin">belanjamu</span>
</h3>

{#each data.userCart as cartItem}
	<CartItemCard {cartItem} />
{/each}

<div
	class="fixed bottom-0 flex h-16 w-full flex-row items-center justify-evenly gap-1 bg-violet-400 text-white md:w-[480px]"
>
	<div>
		<p class="text-sm font-extralight">Total</p>
		<p class="text-lg font-medium">
			Rp{totalPrice.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, '.')}
		</p>
	</div>
</div>
