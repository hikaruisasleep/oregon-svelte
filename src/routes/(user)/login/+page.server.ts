import type { Actions } from '@sveltejs/kit';

export const actions = {
	default: async (e) => {
		const { username, password } = await e.request.formData;
	}
} satisfies Actions;
