<script lang="ts">
	let hasImage = $state(false);
	let previewSrc = $state('');

	function onchange(event: Event) {
		const target = event.target as HTMLInputElement;
		if (target.files) {
			hasImage = true;
			previewSrc = URL.createObjectURL(target.files[0]);
		}
	}
</script>

<label
	for="picture"
	class="mt-2 flex aspect-[7/5] cursor-pointer flex-col items-center justify-center border border-gray-700 p-4"
>
	<input
		type="file"
		name="picture"
		id="picture"
		class="hidden"
		accept="image/jpeg,image/png,image/webp"
		{onchange}
	/>
	<div class:hidden={hasImage}>
		<i class="fa-solid fa-plus fa-2xl leading-normal"></i>
		Tambahkan foto produk
	</div>
	<img class:hidden={!hasImage} src={previewSrc} alt="product" />
</label>
