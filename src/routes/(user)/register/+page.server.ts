import { env } from '$env/dynamic/private';
import type { Actions } from '@sveltejs/kit';

export const actions = {
	default: async (action) => {
		const formData = await action.request.formData();
		const email = formData.get('email');
		const name = formData.get('name');
		const username = formData.get('username');
		const password = formData.get('password');

		const formJson = { email: email, name: name, username: username, password: password };

		const request = await fetch(`${env.API}/user/register`, {
			method: 'POST',
			body: JSON.stringify(formJson)
		});

		if (request.ok) {
			const result: { status: string; userId: string; token: string } = JSON.parse(
				JSON.stringify(await request.json())
			);

			if (result.status == 'Success') {
				action.cookies.set('session_token', result.token, { path: '/' });
				action.cookies.set('user_id', result.userId, { path: '/' });
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
