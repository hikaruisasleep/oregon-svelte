import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load: PageLoad = async ({ parent }) => {
	const { session_token } = await parent();
	if (session_token) {
		redirect(302, '/');
	}
	return { sessionToken: session_token };
};
