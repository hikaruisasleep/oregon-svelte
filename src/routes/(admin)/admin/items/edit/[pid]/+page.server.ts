import { error, redirect, type Actions } from '@sveltejs/kit';
import type { PageServerLoad } from '../$types';
import { env } from '$env/dynamic/private';

export const actions = {
	delete: async (action) => {
		const requestHeaders = new Headers();
		requestHeaders.append('Authorization', `${action.cookies.get('session_token')}`);

		const deleteRequest = await fetch(`${env.API}/product/${action.params.pid}`, {
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

	edit: async (action) => {
		const formData = await action.request.formData();
		const name = formData.get('name')?.toString();
		const price = parseFloat(formData.get('price')?.toString());
		const category = formData.get('category');
		const description = formData.get('description');
		const imageBase64String = formData.get('imageBase64Data')?.toString();
		let imageUrl = '';

		if (imageBase64String) {
			const imageBase64Data = imageBase64String.replace(/^data:.+;base64,/, '');
			const byteCharacters = atob(imageBase64Data);
			const byteNumbers = new Array(byteCharacters.length);
			for (let i = 0; i < byteCharacters.length; i++) {
				byteNumbers[i] = byteCharacters.charCodeAt(i);
			}
			const byteArray = new Uint8Array(byteNumbers);
			const imageFile = new File([byteArray], name || '');

			const imgurFormdata = new FormData();
			imgurFormdata.append('image', imageFile, name || 'imgurimage');
			imgurFormdata.append('type', 'image');
			imgurFormdata.append('title', name || '');
			imgurFormdata.append('description', description || '');

			const requestHeaders = new Headers();
			requestHeaders.append('Authorization', `Client-ID ${env.IMGUR_CLIENTID}`);

			const imgurRequest = await action.fetch('https://api.imgur.com/3/image', {
				method: 'POST',
				headers: requestHeaders,
				body: imgurFormdata,
				redirect: 'follow'
			});
			const imgurResult = await imgurRequest.json();

			imageUrl = imgurResult.data.link;
		}

		const formJson = {
			name,
			price,
			description,
			category,
			imageUrl
		};

		const requestHeaders = new Headers();
		requestHeaders.append('Authorization', `${action.cookies.get('session_token')}`);

		const request = await action.fetch(`${env.API}/product/${action.params.pid}`, {
			method: 'PUT',
			headers: requestHeaders,
			body: JSON.stringify(formJson)
		});

		if (request.ok) {
			const result = await request.json();
			return result;
		} else {
			console.log(JSON.stringify(request));

			return {
				errored: true,
				result: request
			};
		}
	}
} satisfies Actions;

export const load: PageServerLoad = async ({ params }) => {
	const productRequest = await fetch(`${env.API}/product/${params.pid}`, {
		method: 'GET'
	});
	const productResult = (await productRequest.json()).product;
	return { productData: { ...productResult } };
};
