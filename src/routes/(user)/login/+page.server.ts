import { env } from '$env/dynamic/private';
import type { Actions } from '@sveltejs/kit';

export const actions = {
	default: async (action) => {
		const formData = await action.request.formData();
		const email = formData.get('email');
		const password = formData.get('password');

		const formJson = { email: email, password: password };

		const request = await action.fetch(`${env.API}/user/login`, {
			method: 'POST',
			body: JSON.stringify(formJson)
		});

		if (request.ok) {
			const result: { status: string; userId: string; token: string } = await request.json();

			action.cookies.set('session_token', result.token, { path: '/' });
			action.cookies.set('user_id', result.userId, { path: '/' });

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
