import { env } from '$env/dynamic/private';
import type { Actions } from '@sveltejs/kit';

export const actions = {
	default: async (action) => {
		const formData = await action.request.formData();
		const email = formData.get('email');
		const password = formData.get('password');

		const formJson = { email: email, password: password };

		const request = await fetch(`${env.API}/user/login`, {
			method: 'POST',
			body: JSON.stringify(formJson)
		});

		if (request.ok) {
			const result: { status: string; token: string } = JSON.parse(
				JSON.stringify(await request.json())
			);

			if (result.status == 'Success') {
				action.cookies.set('session_token', result.token, { path: '/' });
			}

			return result;
		} else {
			return {
				errored: true,
				status: request.status,
				reason: await request.text()
			};
		}
	}
} satisfies Actions;
