import { defineBoot } from '#q-app'
import { createApi } from '@/composables/api'

export default defineBoot(async ({ store }) => {
  createApi(store)
})
