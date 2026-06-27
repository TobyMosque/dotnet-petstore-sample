import { Pinia } from 'pinia';
import { Configuration, PetApi, StoreApi, UserApi } from './api-cli'
import { useDiStore } from '@/stores/di';

export type { PetApi, StoreApi, UserApi }

declare module 'pinia' {
  export interface PiniaCustomProperties {
    readonly petApi: PetApi;
    readonly storeApi: StoreApi;
    readonly userApi: UserApi;
  }
}

function createConfiguration (pinia: Pinia) {
  return new Configuration({
    basePath: import.meta.env.API_URL,
    fetchApi: fetch
  })
}

export function createApi(pinia: Pinia) {
  const config = createConfiguration(pinia)
  const petApi = new PetApi(config)
  const storeApi = new StoreApi(config)
  const userApi = new UserApi(config)
  pinia.use(() => ({ petApi, storeApi, userApi }))
}

export function usePetApi (pinia?: Pinia) {
  const di = useDiStore(pinia)
  return di.petApi
}

export function useStoreApi (pinia?: Pinia) {
  const di = useDiStore(pinia)
  return di.storeApi
}

export function useUserApi (pinia?: Pinia) {
  const di = useDiStore(pinia)
  return di.userApi
}