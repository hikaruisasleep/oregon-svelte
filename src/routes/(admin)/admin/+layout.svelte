<script lang="ts">
	import '../../../app.css';

	let { children } = $props();
</script>

<svelte:head>
	<title>ecommerce admin</title>
</svelte:head>

<header class="flex h-16 flex-row items-center justify-between px-6 shadow-md">
	<h1>admin mode</h1>
	<nav class="flex justify-between gap-4">
		<a href="/">Return to storefront</a>
	</nav>
</header>

{@render children()}
