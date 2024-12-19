import { env } from '$env/dynamic/private';
import type { LayoutServerLoad } from './$types';

export const load: LayoutServerLoad = async ({ cookies, fetch }) => {
	const sessionToken = cookies.get('session_token');
	const userId = cookies.get('user_id');

	const requestHeaders = new Headers();
	requestHeaders.append('Authorization', sessionToken);

	const isLoggedIn: boolean = sessionToken != undefined;

	let currentUser = {};
	let userCart: any[] = [];
	let isAdmin: boolean = false;
	if (isLoggedIn) {
		const userRequest = await fetch(`${env.API}/user/${userId}`, { method: 'GET' });
		currentUser = await userRequest.json();
		isAdmin = currentUser.role == 1;

		const cartRequest = await fetch(`${env.API}/cart`, {
			method: 'GET',
			headers: requestHeaders
		});
		userCart = await cartRequest.json();
		if (userCart.length > 0) {
			for (const [index, item] of userCart.entries()) {
				const itemRequest = await fetch(`${env.API}/product/${item.productId}`);
				const itemResult = await itemRequest.json();
				userCart[index].product = itemResult.product;
			}
		}
	}

	const productRequest = await fetch(`${env.API}/product`, { method: 'GET' });
	const allProducts = await productRequest.json();

	return {
		session_token: sessionToken,
		isLoggedIn: isLoggedIn,
		allProducts: allProducts,
		currentUser: currentUser,
		userCart: userCart,
		isAdmin: isAdmin
	};
};
