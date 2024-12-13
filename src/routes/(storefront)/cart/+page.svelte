<script lang="ts">
	import type { PageData } from './$types';

	let { data }: { data: PageData } = $props();
</script>

{#each data.userCart as cartItem}
	<p>{JSON.stringify(cartItem)}</p>
{/each}
