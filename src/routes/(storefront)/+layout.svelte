<script lang="ts">
	import '../../app.css';
	let { children } = $props();
</script>

<svelte:head>
	<title>website ecommerce</title>
</svelte:head>

<div class="container flex h-auto w-svw flex-col items-center">
	<div class="container m-0 flex h-full w-[480px] flex-col border border-black p-0">
		<header class="flex h-16 w-full flex-row items-center justify-between px-6 shadow-md">
			<h1>we</h1>
			<nav class="flex justify-between gap-4">
				<a href="/">Home</a>
				<a href="/categories">Categories</a>
				<a href="/me/cart">Cart</a>
				<a href="/me">Profile</a>
			</nav>
		</header>

		{@render children()}
	</div>
</div>
