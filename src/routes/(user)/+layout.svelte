<script lang="ts">
	import '../../app.css';
	let { children } = $props();
</script>

<svelte:head>
	<title>0regon</title>
</svelte:head>

<div class="flex h-screen w-svw flex-col content-center items-center overflow-hidden">
	<div class="container m-0 flex h-full w-full flex-col p-0 md:w-[480px]">
		{@render children()}
	</div>
</div>

<style lang="scss">
	:global {
		h1 {
			@apply font-thin;
		}

		input {
			@apply h-10;
		}

		label {
			@apply translate-x-1;
			@apply translate-y-3;
			@apply bg-white;
			@apply w-fit;
			@apply px-1;
		}
	}
</style>
