<script lang="ts">
	import ItemImage from '$lib/components/ItemImage.svelte';
	import type { ActionData } from '../$types';

	let { form }: { form: ActionData } = $props();
</script>

<div class="add-product m-4 flex flex-col items-center">
	<div class="mb-4 flex w-full flex-col">
		<div class="flex w-full flex-row items-end justify-between">
			<h3 class="text-xl font-thin">Tambah produk baru</h3>
			<a href="/admin/items" class="flex flex-row justify-end gap-1 text-red-600">
				<i class="fa-solid fa-chevron-left leading-normal"></i>
				Back
			</a>
		</div>
		{#if form?.message}
			<p class="self-start text-xs text-green-800">Berhasil menambahkan produk</p>
		{/if}
	</div>

	<form action="" method="post" class="add-form flex w-full flex-col gap-2">
		<label for="name">Nama produk</label>
		<input type="text" name="name" id="name" class="h-8" value="" />

		<label for="price">Harga produk</label>
		<input type="number" name="price" id="price" class="h-8" value="" />

		<label for="category">Kategori</label>
		<input type="text" name="category" id="category" class="h-8" value="" />

		<label for="description">Deskripsi</label>
		<textarea name="description" id="description" rows="5" value=""></textarea>

		<ItemImage></ItemImage>

		<input
			type="submit"
			value="Tambah Produk"
			class="mt-10 w-fit self-center rounded-full bg-green-900 px-4 py-2 text-white"
		/>
	</form>

	{#if form?.errored}
		{JSON.stringify(form)}
	{/if}
</div>
