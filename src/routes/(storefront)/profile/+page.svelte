<script lang="ts">
	import type { PageServerData } from '../$types';

	let { data }: { data: PageServerData } = $props();
</script>

<div class="h-56 bg-violet-600"></div>

<div class="flex flex-row justify-start">
	<div class="aspect-square w-32 -translate-y-16 translate-x-4 rounded-full bg-violet-400"></div>
	<div class="ml-6 mt-3">
		<h1 class="text-4xl">{data.currentUser.username}</h1>
	</div>
</div>
<div class="min-h-8 w-full bg-slate-200">
	{#each JSON.stringify(data.currentUser).replace('{', '').replace('}', '').split(',') as entry}
		<p>{entry}</p>
	{/each}
</div>

<a href="/profile/edit" class="w-fit rounded bg-indigo-500 px-3 py-2 text-white">Edit profile</a>
<button class="w-fit rounded bg-red-600 px-3 py-2 text-white">Log out</button>
