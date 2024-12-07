<script lang="ts">
	import DiscussionStarterCard from '$lib/components/DiscussionStarterCard.svelte';
	import ItemCard from '$lib/components/ItemCard.svelte';

	let expandDescription = $state(false);
</script>

<div class="aspect-[5/4] w-full bg-violet-400"></div>

<div class="mb-16 mt-2 flex flex-col gap-4 px-4">
	<div class="flex items-start justify-between">
		<div class="flex flex-col items-start justify-center">
			<h1 class="text-3xl font-light">Nama produk</h1>
			<p>Rp50.000</p>
		</div>
		<p class="mt-2 text-right">
			4.8/5 <i class="fa-solid fa-star text-orange-400 drop-shadow-sm"></i>
		</p>
	</div>
	<div class="flex flex-row items-center justify-start text-xs">
		<p>Dilihat <span class="text-purple-900">100</span></p>
		<span class="mx-2 h-4 w-[1px] bg-black"></span>
		<p>Terjual <span class="text-purple-900">35</span></p>
	</div>
	<hr />
	<div class="flex flex-col">
		<h3 class="mb-1 text-lg font-medium">Detail produk</h3>
		<div
			class="description-text text-justify text-sm leading-snug"
			class:expanded={expandDescription}
		>
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
	<div class="flex flex-col">
		<div class="mb-2 flex flex-row items-end justify-between">
			<h3 class="text-lg font-medium">Diskusi</h3>
			<a href="discussions" class="text-sm leading-[1.5rem] text-violet-400"> Lihat semua </a>
		</div>
		<div class="flex flex-col gap-2">
			<DiscussionStarterCard></DiscussionStarterCard>
			<DiscussionStarterCard></DiscussionStarterCard>
			<DiscussionStarterCard></DiscussionStarterCard>
		</div>
	</div>
	<div class="flex flex-col">
		<div class="mb-2 flex flex-row items-end justify-between">
			<h3 class="text-lg font-medium">Produk Serupa</h3>
		</div>
		<div class="flex flex-row gap-4 overflow-scroll">
			<ItemCard></ItemCard>
			<ItemCard></ItemCard>
			<ItemCard></ItemCard>
			<ItemCard></ItemCard>
			<ItemCard></ItemCard>
		</div>
	</div>
</div>

<div
	class="fixed bottom-0 flex h-12 w-full flex-row items-center justify-evenly gap-1 bg-violet-400 text-white md:w-[480px]"
>
	<button aria-label="chat seller"><i class="fa-regular fa-comments"></i></button>
	<span class="mx-1 inline-block h-4/5 w-[1px] bg-violet-500 drop-shadow"></span>
	<button>
		<p>Beli Sekarang</p>
	</button>
	<span class="mx-1 inline-block h-4/5 w-[1px] bg-violet-500 drop-shadow"></span>
	<button class="flex flex-row justify-center gap-[0.35ch]">
		<i class="fa-solid fa-plus fa-sm leading-relaxed"></i>
		<p>Keranjang</p>
	</button>
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
