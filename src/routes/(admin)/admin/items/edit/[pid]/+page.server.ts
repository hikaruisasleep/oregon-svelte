import { API } from '$env/static/private';
import { error, redirect, type Actions } from '@sveltejs/kit';
import type { PageServerLoad } from '../$types';

export const actions = {
	delete: async (action) => {
		const requestHeaders = new Headers();
		requestHeaders.append('Authorization', `${action.cookies.get('session_token')}`);

		const deleteRequest = await fetch(`${API}/product/${action.params.pid}`, {
			method: 'DELETE',
			headers: requestHeaders
		});
		const deleteResponse = await deleteRequest.json();

		if (deleteResponse.ok) {
			redirect(302, '/admin/items');
		} else {
			error(500, JSON.stringify(deleteResponse));
		}
	},

	edit: async () => {}
} satisfies Actions;

export const load: PageServerLoad = async ({ params }) => {
	const productRequest = await fetch(`${API}/product/${params.pid}`, {
		method: 'GET'
	});
	const productResult = (await productRequest.json()).product;
	return { productData: { ...productResult } };
};
