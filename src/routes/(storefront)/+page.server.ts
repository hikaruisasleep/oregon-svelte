import { redirect, type Actions } from '@sveltejs/kit';

export const actions = {
	logout: async (action) => {
		action.cookies.delete('session_token', { path: '/' });

		throw redirect(302, '/');
	}
} satisfies Actions;
