import { env } from '$env/dynamic/private';
import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async ({ url }) => {
	const searchTerm = url.searchParams.get('term');
	const searchRequest = await fetch(`${env.API}/product?name=${searchTerm}`);
	const searchResult = await searchRequest.json();

	return { searchTerm: searchTerm, searchResult: searchResult };
};
