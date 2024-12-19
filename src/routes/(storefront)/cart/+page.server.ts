import { redirect, type Actions } from '@sveltejs/kit';
import type { PageServerLoad } from '../$types';
import { env } from '$env/dynamic/private';

export const load: PageServerLoad = async ({ cookies }) => {
	const sessionToken = cookies.get('session_token');

	if (!sessionToken) {
		redirect(302, '/login');
	}
};

export const actions = {
	updatecart: async (action) => {
		const sessionToken = action.cookies.get('session_token');

		if (!sessionToken) {
			redirect(302, '/login');
		}

		const formData = await action.request.formData();
		const pid = formData.get('product-id');
		const cid = formData.get('cart-id');
		const qstr = formData.get(`qty-${pid}`)?.toString();
		const quantity = qstr ? parseInt(qstr) : 0;

		const formJson = { quantity: quantity };

		const requestHeaders = new Headers();
		requestHeaders.append('Authorization', sessionToken);

		const updateRequest = await fetch(`${env.API}/cart/${cid}`, {
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
