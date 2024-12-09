<script lang="ts">
	import ItemImage from '$lib/components/ItemImage.svelte';
	import type { PageServerData } from '../$types';

	let { data }: { data: PageServerData } = $props();
</script>

<div class="add-product m-4 flex flex-col items-center">
	<div class="mb-4 self-start">
		<h3 class="text-xl font-thin">Edit produk</h3>
	</div>

	<p>{JSON.stringify(data.productData)}</p>

	<form action="/?edit" method="post" class="add-form flex w-full flex-col gap-2">
		<label for="name">Nama produk</label>
		<input type="text" name="name" id="name" class="h-8" value={data.productData.name} />

		<label for="price">Harga produk</label>
		<input type="number" name="price" id="price" class="h-8" value={data.productData.price} />

		<label for="description">Deskripsi</label>
		<textarea name="description" id="description" rows="5" value={data.productData.description}
		></textarea>

		<ItemImage></ItemImage>

		<input
			type="submit"
			value="Edit"
			class="mt-10 w-fit self-center rounded-full bg-green-900 px-4 py-2 text-white"
		/>
	</form>
</div>

<style>
	input[type='number'] {
		appearance: textfield;
	}

	input[type='number']::-webkit-inner-spin-button,
	input[type='number']::-webkit-outer-spin-button {
		-webkit-appearance: none;
		margin: 0;
	}
</style>
