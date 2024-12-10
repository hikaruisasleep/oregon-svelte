<script>
	let props = $props();
	let user = props.user;
</script>

<div
	class="grid h-24 w-full flex-shrink-0 flex-grow-0 grid-cols-4 grid-rows-1 rounded border border-black"
>
	<div class="col-span-2 flex flex-col justify-between px-3 py-3 shadow-inner">
		<div>
			<p class="font-light">{user.username}</p>
		</div>
	</div>
</div>
