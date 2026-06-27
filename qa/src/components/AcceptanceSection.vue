<template>
  <section class="qa-section">
    <div class="qa-section-header">
      <h2 class="qa-section-title">{{ title }}</h2>
      <slot name="actions">
        <q-btn flat no-caps dense size="sm" label="Run" @click="runGroup(name)" />
      </slot>
    </div>
    <slot />
  </section>
</template>

<script setup lang="ts">
import { provide } from 'vue'
import { useAcceptanceState } from '@/composables/acceptance'

const props = defineProps<{ title: string; name: string }>()
const { runGroup } = useAcceptanceState()

provide('acceptance:group', props.name)
</script>
