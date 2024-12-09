import { API } from '$env/static/private';
import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async ({ params }) => {
	const productRequest = await fetch(`${API}/product/${params.pid}`, {
		method: 'GET'
	});
	const productResult = await productRequest.json();

	return { productResult };
};
