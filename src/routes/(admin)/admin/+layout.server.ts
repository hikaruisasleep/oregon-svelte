import { env } from '$env/dynamic/private';
import type { LayoutServerLoad } from './$types';

export const load: LayoutServerLoad = async ({ cookies }) => {
	const userId = cookies.get('user_id');
	const userRequest = await fetch(`${env.API}/user/${userId}`, { method: 'GET' });
	const currentUser = await userRequest.json();

	const productRequest = await fetch(`${env.API}/product`, { method: 'GET' });
	const allProducts = await productRequest.json();

	return { currentUser, allProducts };
};
