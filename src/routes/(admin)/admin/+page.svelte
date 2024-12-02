<script lang="ts">
	import EditableItemCard from '$lib/components/EditableItemCard.svelte';
</script>

<div class="products m-4 flex flex-col items-center">
	<div class="self-start">
		<h3 class="text-xl">List produk</h3>
	</div>
	<div class="mb-6 mt-2 w-fit rounded-lg bg-lime-800 px-3 py-2 text-white">
		<a href="admin/add">
			<i class="fa-solid fa-square-plus mr-1 leading-normal"></i>
			Tambah produk baru
		</a>
	</div>
	<div class="products-items flex w-full flex-col gap-2 overflow-scroll">
		<EditableItemCard></EditableItemCard>
		<EditableItemCard></EditableItemCard>
		<EditableItemCard></EditableItemCard>
		<EditableItemCard></EditableItemCard>
		<EditableItemCard></EditableItemCard>
		<EditableItemCard></EditableItemCard>
		<EditableItemCard></EditableItemCard>
		<EditableItemCard></EditableItemCard>
		<EditableItemCard></EditableItemCard>
		<EditableItemCard></EditableItemCard>
	</div>
</div>
