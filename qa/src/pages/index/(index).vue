<template>
  <q-page class="qa-page">
    <AcceptanceRoot>

      <AcceptanceSection title="Pets" name="pets">
        <AcceptanceNamed
          id="pet-find-by-status-available"
          label='List pets with status "available"'
          endpoint="/pet/findByStatus?status=available"
          :run="() => petApi.petFindByStatusGet({ status: 'available' })"
          :assert="r => Array.isArray(r) ? null : 'Expected an array'"
        />
        <AcceptanceNamed
          id="pet-find-by-status-pending"
          label='List pets with status "pending"'
          endpoint="/pet/findByStatus?status=pending"
          :run="() => petApi.petFindByStatusGet({ status: 'pending' })"
          :assert="r => Array.isArray(r) ? null : 'Expected an array'"
        />
        <AcceptanceNamed
          id="pet-find-by-status-no-param"
          label="List all pets without status filter — model-binding regression"
          endpoint="/pet/findByStatus"
          :run="() => petApi.petFindByStatusGet({})"
          :assert="r => Array.isArray(r) ? null : 'Expected an array'"
        />
        <AcceptanceNamed
          id="pet-find-by-tags"
          label='Find pets by tag "young" — EF materialization regression'
          endpoint="/pet/findByTags?tags=young"
          :run="() => petApi.petFindByTagsGet({ tags: ['young'] })"
          :assert="r => Array.isArray(r) ? null : 'Expected an array'"
        />
      </AcceptanceSection>

      <AcceptanceSection title="Store" name="store">
        <AcceptanceNamed
          id="store-inventory"
          label="Get inventory counts by status"
          endpoint="/store/inventory"
          :run="() => storeApi.storeInventoryGet()"
          :assert="r => r && typeof r === 'object' ? null : 'Expected an object'"
        />
        <AcceptanceNamed
          id="store-orders"
          label="List all orders — EF materialization regression"
          endpoint="/store/order"
          :run="() => storeApi.storeOrderGet()"
          :assert="r => Array.isArray(r) ? null : 'Expected an array'"
        />
      </AcceptanceSection>

      <AcceptanceSection title="Users" name="users">
        <AcceptanceNamed
          id="user-list"
          label="List users (skip=0, take=5)"
          endpoint="/user?skip=0&take=5"
          :run="fetchUsers"
          :assert="r => Array.isArray(r) ? null : 'Expected an array'"
          @success="onUserListSuccess"
        />
        <AcceptanceNamed
          id="user-get-by-name"
          label="Get first user from list — chained from user-list"
          :endpoint="() => `/user/${username || ':username from list'}`"
          :run="() => userApi.userUsernameGet({ username })"
          :assert="r => (r as { id?: string })?.id ? null : 'Expected a user object with id'"
        />
      </AcceptanceSection>

    </AcceptanceRoot>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import AcceptanceRoot from '@/components/AcceptanceRoot.vue'
import AcceptanceSection from '@/components/AcceptanceSection.vue'
import AcceptanceNamed from '@/components/AcceptanceNamed.vue'
import { usePetApi, useStoreApi, useUserApi } from '@/composables/api'

const petApi = usePetApi()
const storeApi = useStoreApi()
const userApi = useUserApi()

const username = ref('')

const apiUrl = import.meta.env.API_URL as string

async function fetchUsers() {
  const r = await fetch(`${apiUrl}/user?skip=0&take=5`)
  if (!r.ok) throw new Error(`${r.status} ${r.statusText}`)
  return r.json() as Promise<Array<{ username?: string | null }>>
}

function onUserListSuccess(data: unknown) {
  const users = data as Array<{ username?: string | null }>
  username.value = users?.[0]?.username ?? ''
}
</script>
