import type { LayoutServerLoad } from './$types';

export const load: LayoutServerLoad = ({ cookies }) => {
	const sessionToken = cookies.get('session_token');

	return {
		session_token: sessionToken
	};
};
