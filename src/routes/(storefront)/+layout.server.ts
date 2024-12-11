import { env } from '$env/dynamic/private';
import type { LayoutServerLoad } from './$types';

export const load: LayoutServerLoad = async ({ cookies, fetch }) => {
	const sessionToken = cookies.get('session_token');
	const userId = cookies.get('user_id');

	const isLoggedIn: boolean = sessionToken != undefined;

	let currentUser = {};
	let isAdmin: boolean = false;
	if (isLoggedIn) {
		const userRequest = await fetch(`${env.API}/user/${userId}`, { method: 'GET' });
		currentUser = await userRequest.json();
		isAdmin = currentUser.role == 1;
	}

	const productRequest = await fetch(`${env.API}/product`, { method: 'GET' });
	const allProducts = await productRequest.json();
	return {
		session_token: sessionToken,
		isLoggedIn: isLoggedIn,
		allProducts: allProducts,
		currentUser: currentUser,
		isAdmin: isAdmin
	};
};
