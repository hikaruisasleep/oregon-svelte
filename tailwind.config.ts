import forms from '@tailwindcss/forms';
import typography from '@tailwindcss/typography';
import type { Config } from 'tailwindcss';

export default {
	content: ['./src/**/*.{html,js,svelte,ts}'],

	theme: {
		extend: {},
		fontFamily: {
			sans: ['Commissioner'],
			serif: ['DM Serif Text']
		}
	},

	plugins: [typography, forms]
} satisfies Config;
