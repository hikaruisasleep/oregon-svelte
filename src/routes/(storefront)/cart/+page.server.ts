import { redirect, type Actions } from '@sveltejs/kit';
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

export const actions = {
	updatecart: async (action) => {
		const sessionToken = action.cookies.get('session_token');
		const formData = await action.request.formData();
		const pid = formData.get('product-id');
		const quantity = formData.get(`qty-${pid}`);

		const formJson = { id: pid, quantity: quantity };

		if (!sessionToken) {
			redirect(302, '/login');
		}

		const requestHeaders = new Headers();
		requestHeaders.append('Authorization', sessionToken);

		const updateRequest = await fetch(`${env.API}/cart/${pid}`, {
			method: 'PUT',
			headers: requestHeaders,
			body: JSON.stringify(formJson)
		});

		const updateResult = await updateRequest.json();

		if (updateResult.ok) {
			return { updateRequest };
		}
	},
	checkout: async (action) => {
		const sessionToken = action.cookies.get('session_token');

		console.log(`token: ${sessionToken}`);

		const requestHeaders = new Headers();
		requestHeaders.append('Authorization', sessionToken);

		const checkoutRequest = await fetch(`${env.API}/cart/checkout`, {
			method: 'POST',
			headers: requestHeaders
		});

		const checkoutResponse = await checkoutRequest.json();

		if (checkoutResponse.ok) {
			redirect(302, '/cart/checkout');
		} else {
			console.log(checkoutResponse);
		}
	}
} satisfies Actions;
