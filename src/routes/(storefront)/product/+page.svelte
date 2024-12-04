<script lang="ts">
	let expandDescription = $state(false);
</script>

<div class="aspect-[5/4] w-full bg-violet-400"></div>

<div class="mt-2 flex flex-col gap-4 px-4">
	<div class="flex items-start justify-between">
		<div class="flex flex-col items-start justify-center">
			<h1 class="text-3xl font-light">Nama produk</h1>
			<p>Rp50.000</p>
		</div>
		<p class="mt-2 text-right">
			4.8/5 <i class="fa-solid fa-star text-orange-400 drop-shadow-sm"></i>
		</p>
	</div>
	<hr />
	<div class="flex flex-col">
		<div class="description-text text-justify leading-snug" class:expanded={expandDescription}>
			Lorem ipsum dolor sit amet, consectetur adipisicing elit. Aspernatur iste nostrum enim.
			Facilis magni voluptates voluptate assumenda inventore quisquam optio sint voluptas impedit
			porro hic nostrum dolorem, similique explicabo! Impedit. Lorem ipsum, dolor sit amet
			consectetur adipisicing elit. Odio dolorem laudantium distinctio quam quos aspernatur voluptas
			veritatis sequi dolorum saepe consequuntur autem, ad corrupti voluptates rem accusantium enim
			quo ipsum. Lorem ipsum dolor, sit amet consectetur adipisicing elit. Eaque dolor architecto
			blanditiis reiciendis, laudantium, enim omnis atque quia deleniti earum dignissimos facilis
			beatae esse inventore adipisci nesciunt nobis at? Assumenda?
		</div>
		<button
			class="flex h-8 -translate-y-8 items-end justify-center bg-gradient-to-b from-transparent"
			aria-label="expand description"
			onclick={() => {
				expandDescription = !expandDescription;
			}}
			class:to-transparent={expandDescription}
			class:to-white={!expandDescription}
		>
			<i
				class="fa-solid fa-angles-down bottom-0 translate-y-6 leading-none"
				class:rotate-180={expandDescription}
			></i>
		</button>
	</div>
	<hr />
	<div>
		Lorem ipsum dolor sit amet consectetur, adipisicing elit. Suscipit sapiente distinctio tempore!
		Repellat porro fuga assumenda nihil vero cum impedit unde, modi labore recusandae voluptate
		officia? Doloribus ut maiores minima?
	</div>
</div>

<div class="fixed bottom-0 h-12 w-full bg-violet-400">
	<p>add to cart</p>
</div>

<style>
	.description-text {
		@apply h-32;
		@apply overflow-hidden;
	}

	.expanded {
		@apply h-max;
		@apply overflow-auto;
	}
</style>
