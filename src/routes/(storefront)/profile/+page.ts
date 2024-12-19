import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load: PageLoad = async ({ parent }) => {
	const { isLoggedIn } = await parent();
	if (!isLoggedIn) {
		redirect(307, '/login');
	}
};
