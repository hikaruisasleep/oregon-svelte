import { env } from '$env/dynamic/private';
import type { PageServerLoad } from '../$types';

export const load: PageServerLoad = async ({ cookies }) => {
	const userId = await cookies.get('user_id');
	const userRequest = await fetch(`${env.API}/user/${userId}`);
	const currentUser = await userRequest.json();

	return { currentUser };
};
