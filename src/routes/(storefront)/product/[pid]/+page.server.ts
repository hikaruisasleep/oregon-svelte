import { redirect, type Actions } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';
import { env } from '$env/dynamic/private';

export const load: PageServerLoad = async ({ params, fetch }) => {
	const productRequest = await fetch(`${env.API}/product/${params.pid}`, {
		method: 'GET'
	});
	const productResult = await productRequest.json();

	return { productResult };
};

export const actions = {
	default: async (action) => {
		const sessionToken = action.cookies.get('session_token');

		const requestHeaders = new Headers();
		requestHeaders.append('Authorization', sessionToken);

		const jsonBody = {
			cart: {
				productId: parseInt(action.params.pid),
				quantity: 1
			}
		};

		const addToCartRequest = await fetch(`${env.API}/cart`, {
			method: 'POST',
			headers: requestHeaders,
			body: JSON.stringify(jsonBody)
		});

		if (addToCartRequest.status == 401) {
			redirect(302, '/login');
		}

		if (addToCartRequest.ok) {
			redirect(301, `/products/${action.params.pid}`);
		}
	}
} satisfies Actions;
