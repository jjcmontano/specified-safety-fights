module.exports = {
    env: {
        browser: true,
        es2021: true,
        'jest/globals': true
    },
    extends: [
        'eslint:recommended',
        'plugin:react/recommended',
        'airbnb',
        'prettier',
    ],
    "overrides": [
        {
            "files": ["test/**"],
            "plugins": ["jest"],
            "extends": ["plugin:jest/recommended"],
            "rules": { "jest/prefer-expect-assertions": "off" }
        }
    ],
    parserOptions: {
        ecmaVersion: 'latest',
        sourceType: 'module',
        ecmaFeatures: {
            jsx: true,
        },
    },
    plugins: [
        'react',
        'jest'
    ],
    rules: {
        'linebreak-style': ['error', 'windows'],
        indent: ['error', 4],
        'jest/no-disabled-tests': 'warn',
        'jest/no-focused-tests': 'error',
        'jest/no-identical-title': 'error',
        'jest/prefer-to-have-length': 'warn',
        'jest/valid-expect': 'error'
    },
};
