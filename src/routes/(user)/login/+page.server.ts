import type { Actions } from '@sveltejs/kit';

export const actions = {
	default: async (e) => {
		const formData = await e.request.formData();
		const email = formData.get('email');
		const password = formData.get('password');

		return { email: email, password: password };
	}
} satisfies Actions;
