import { redirect } from '@sveltejs/kit';
import type { PageServerLoad } from '../$types';
import { env } from '$env/dynamic/private';

export const load: PageServerLoad = async ({ cookies }) => {
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
	let userCart = await cartRequest.json();

	for (const [index, item] of userCart.entries()) {
		const itemRequest = await fetch(`${env.API}/product/${item.productId}`);
		const itemResult = await itemRequest.json();

		userCart[index].product = itemResult.product;
	}

	return { userCart };
};
