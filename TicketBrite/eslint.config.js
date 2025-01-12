import js from '@eslint/js'
import globals from 'globals'
import reactHooks from 'eslint-plugin-react-hooks'
import reactRefresh from 'eslint-plugin-react-refresh'
import tseslint from 'typescript-eslint'
import cypress from 'cypress'

export default tseslint.config(

    { ignores: ['dist'] },
    {
        env: {
            "cypress/globals": true,
        },
        extends: [js.configs.recommended, ...tseslint.configs.recommended],
        files: ['**/*.{ts,tsx}'],
        languageOptions: {
            ecmaVersion: 2020,
            globals: globals.browser,
        },
        plugins: {
            'react-hooks': reactHooks,
            'react-refresh': reactRefresh,
            'cypress': cypress
        },
        rules: {
            "no-restricted-syntax": [
                "error",
                {
                    "selector": "CallExpression[callee.name='fetch'][arguments.0.value=/^http:/]",
                    "message": "HTTP requests are not recommended for security reasons."
                }
            ],
            'react-refresh/only-export-components': [
                'warn',
                { allowConstantExport: true },
            ],
        },
    },
)
