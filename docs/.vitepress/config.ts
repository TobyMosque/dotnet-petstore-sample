import { defineConfig } from 'vitepress'

export default defineConfig({
  title: 'PetStore API',
  description: 'User guide and acceptance criteria for the PetStore REST API',
  themeConfig: {
    nav: [
      { text: 'Guide', link: '/guide/getting-started' },
      { text: 'API Reference', link: '/api-reference/pets' },
      { text: 'Acceptance Criteria', link: '/acceptance/' },
    ],
    sidebar: [
      {
        text: 'Guide',
        items: [
          { text: 'Getting Started', link: '/guide/getting-started' },
          { text: 'Data Model', link: '/guide/data-model' },
        ],
      },
      {
        text: 'Concepts',
        items: [
          { text: 'Pets', link: '/concepts/pets' },
          { text: 'Store & Orders', link: '/concepts/store' },
          { text: 'Users', link: '/concepts/users' },
        ],
      },
      {
        text: 'API Reference',
        items: [
          { text: 'Pets', link: '/api-reference/pets' },
          { text: 'Store', link: '/api-reference/store' },
          { text: 'Users', link: '/api-reference/users' },
          { text: 'Data', link: '/api-reference/data' },
        ],
      },
      {
        text: 'Acceptance Criteria',
        items: [
          { text: 'Overview', link: '/acceptance/' },
          { text: 'Pet Management', link: '/acceptance/pet-management' },
          { text: 'Store & Orders', link: '/acceptance/store' },
          { text: 'User Management', link: '/acceptance/users' },
        ],
      },
    ],
  },
})
