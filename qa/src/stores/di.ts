import { defineStore, acceptHMRUpdate } from 'pinia';

export const useDiStore = defineStore('di', () => ({}))

if (import.meta.hot) {
  import.meta.hot.accept(acceptHMRUpdate(useDiStore, import.meta.hot));
}
