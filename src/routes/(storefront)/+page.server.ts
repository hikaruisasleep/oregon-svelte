import { redirect, type Actions } from '@sveltejs/kit';
import { setContext } from 'svelte';

export const actions = {
	logout: async (action) => {
		action.cookies.delete('session_token', { path: '/' });
		setContext('logged_in', false);

		throw redirect(302, '/');
	}
} satisfies Actions;
