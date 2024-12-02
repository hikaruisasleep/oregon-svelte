<script lang="ts">
	import '../../app.css';
	let { children } = $props();
</script>

<svelte:head>
	<title>0regon</title>
</svelte:head>

<div class="flex h-auto w-svw flex-col content-center items-center overflow-hidden">
	<div class="container m-0 flex h-full w-full flex-col border border-black p-0 md:w-[480px]">
		<header class="flex h-16 w-full flex-row items-center justify-between px-6 shadow-md">
			<a href="admin">
				<h1 class="font-serif text-2xl md:text-3xl">0regon</h1>
			</a>
			<nav class="flex justify-between gap-2 md:gap-4">
				<a href="/">Home</a>
				<a href="/">Categories</a>
				<a href="/">Cart</a>
				<a href="/">Profile</a>
			</nav>
		</header>

		{@render children()}
	</div>
</div>
