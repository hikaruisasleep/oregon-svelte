import { redirect, type Actions } from '@sveltejs/kit';
import { setContext } from 'svelte';
import type { PageServerLoad } from './$types';
import { env } from '$env/dynamic/private';

export const actions = {
	logout: async (action) => {
		action.cookies.delete('session_token', { path: '/' });
		setContext('logged_in', false);

		throw redirect(302, '/');
	}
} satisfies Actions;

export const load: PageServerLoad = async () => {
	const productRequest = await fetch(`${env.API}/product`, { method: 'GET' });
	const allProducts = await productRequest.json();

	return { allProducts };
};
