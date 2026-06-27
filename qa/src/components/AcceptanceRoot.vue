<template>
  <div>
    <div class="qa-run-all-row">
      <div class="qa-summary">
        <span>{{ summary.total }} tests</span>
        <span v-if="summary.pass" class="qa-summary-pass">{{ summary.pass }} passed</span>
        <span v-if="summary.fail" class="qa-summary-fail">{{ summary.fail }} failed</span>
      </div>
      <q-btn flat no-caps dense label="Run all" :loading="allRunning" @click="runAll()" />
    </div>
    <slot />
  </div>
</template>

<script setup lang="ts">
import { provideAcceptance } from '@/composables/acceptance'

const { summary, allRunning, runAll } = provideAcceptance()!
</script>
