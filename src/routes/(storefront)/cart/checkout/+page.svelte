<script>
	import { goto } from '$app/navigation';

	let timer = $state(5);

	let countdown = setInterval(() => {
		timer--;
	}, 1000);

	function clearCountdownThenRedirect() {
		clearInterval(countdown);
		goto('/');
	}
</script>

<div class="flex h-svh w-svw flex-col items-center justify-center">
	<h1>Sucessfully checked out!</h1>
	<p>Returning to homepage in {timer}</p>
	{#if timer == 0}{clearCountdownThenRedirect()}{/if}
</div>
