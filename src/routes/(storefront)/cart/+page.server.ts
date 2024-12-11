import { redirect } from '@sveltejs/kit';
import type { PageServerLoad } from '../$types';
import { env } from '$env/dynamic/private';

const load: PageServerLoad = async ({ cookies }) => {
	const sessionToken = cookies.get('session_token');

	if (!sessionToken) {
		redirect(302, '/login');
	}

	const requestHeaders = new Headers();
	requestHeaders.append('Authorization', sessionToken);

	const cartRequest = await fetch(`${env.API}/cart`, {
		method: 'GET',
		headers: requestHeaders
	});
	const userCart = await cartRequest.json();

	return { userCart };
};
