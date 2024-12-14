<script lang="ts">
	import ItemImage from '$lib/components/ItemImage.svelte';
	import type { PageServerData, Action } from './$types';

	let { data, form }: { data: PageServerData; form: Action } = $props();
</script>

<div class="add-product m-4 flex flex-col items-center">
	<div class="mb-4 flex w-full flex-col">
		<div class="flex w-full flex-row items-end justify-between">
			<h3 class="text-xl font-thin">Edit produk</h3>
			<a href="/admin/items" class="flex flex-row justify-end gap-1 text-red-600">
				<i class="fa-solid fa-chevron-left leading-normal"></i>
				Back
			</a>
		</div>
		{#if form?.message}
			<p class="self-start text-xs text-green-800">Berhasil mengedit produk</p>
		{/if}
	</div>

	<form action="?/edit" method="post" class="add-form flex w-full flex-col gap-2">
		<label for="name">Nama produk</label>
		<input type="text" name="name" id="name" class="h-8" value={data.productData.name} />

		<label for="price">Harga produk</label>
		<input type="number" name="price" id="price" class="h-8" value={data.productData.price} />

		<label for="category">Kategori</label>
		<input
			type="text"
			name="category"
			id="category"
			class="h-8"
			value={data.productData.category}
		/>

		<label for="description">Deskripsi</label>
		<textarea name="description" id="description" rows="5" value={data.productData.description}
		></textarea>

		<ItemImage existingImage={data.productData.imageUrl} />

		<div class="mt-10 flex flex-row justify-center gap-4 self-center text-white">
			<input
				type="submit"
				value="Delete"
				formaction="?/delete"
				class="w-fit rounded-full bg-red-600 px-4 py-2"
			/>

			<input type="submit" value="Confirm Edit" class="w-fit rounded-full bg-green-900 px-4 py-2" />
		</div>
	</form>
</div>
