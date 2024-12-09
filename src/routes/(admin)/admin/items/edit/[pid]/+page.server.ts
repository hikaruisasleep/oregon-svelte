import { API } from '$env/static/private';
import { error, type Actions } from '@sveltejs/kit';
import type { PageServerLoad } from '../$types';

export const actions = {
	delete: async (action) => {
		const deleteRequest = await fetch(`${API}/product/${action.params.pid}`, {
			method: 'DELETE'
		});
		const deleteResponse = await deleteRequest.json();

		if (deleteResponse.ok) {
			return deleteResponse;
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
