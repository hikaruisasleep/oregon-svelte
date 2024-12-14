import { env } from '$env/dynamic/private';
import { redirect } from '@sveltejs/kit';
import type { LayoutServerLoad } from './$types';

export const load: LayoutServerLoad = async ({ cookies, fetch }) => {
	if (!cookies.get('session_token')) {
		redirect(302, '/login');
	}

	const requestHeaders = new Headers();
	requestHeaders.append('Authorization', `${cookies.get('session_token')}`);

	const userId = cookies.get('user_id');
	const userRequest = await fetch(`${env.API}/user/${userId}`, { method: 'GET' });
	const currentUser = await userRequest.json();

	const allUserRequest = await fetch(`${env.API}/user`, { method: 'GET', headers: requestHeaders });
	const allUsers = await allUserRequest.json();

	const productRequest = await fetch(`${env.API}/product`, { method: 'GET' });
	const allProducts = await productRequest.json();

	return { currentUser, allProducts, allUsers };
};
