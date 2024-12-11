import { env } from '$env/dynamic/private';
import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async ({ fetch }) => {
	const productRequest = await fetch(`${env.API}/product`, { method: 'GET' });
	const allProducts = await productRequest.json();

	return { allProducts };
};
