<script lang="ts">
	import ItemImage from '$lib/components/ItemImage.svelte';
</script>

<div class="add-product m-4 flex flex-col items-center">
	<div class="mb-4 self-start">
		<h3 class="text-xl font-thin">Edit produk</h3>
	</div>

	<form action="" method="post" class="add-form flex w-full flex-col gap-2">
		<label for="name">Nama produk</label>
		<input type="text" name="name" id="name" class="h-8" />

		<label for="price">Harga produk</label>
		<input type="number" name="price" id="price" class="h-8" />

		<label for="description">Deskripsi</label>
		<textarea name="description" id="description" rows="5"></textarea>

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
