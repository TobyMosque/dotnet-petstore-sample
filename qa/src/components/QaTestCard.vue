<template>
  <div class="qa-card" :class="`qa-card--${result.status}`">
    <button class="qa-card-trigger" @click="toggle">
      <span class="qa-method">{{ test.method }}</span>
      <span class="qa-endpoint">{{ test.endpoint }}</span>
      <span class="qa-card-right">
        <span v-if="result.durationMs" class="qa-duration">{{ result.durationMs }}ms</span>
        <span v-if="result.status === 'running'" class="qa-spin">
          <q-spinner size="10px" color="info" />
        </span>
        <span v-else class="qa-status-dot" :class="`qa-status-dot--${result.status}`" />
        <span class="qa-status-text" :class="`qa-status-text--${result.status}`">
          {{ STATUS_LABELS[result.status] }}
        </span>
        <q-btn
          flat dense round size="xs"
          icon="play_arrow"
          :disable="result.status === 'running'"
          @click.stop="$emit('run')"
        />
      </span>
    </button>
    <div class="qa-card-label">{{ test.label }}</div>
    <div v-if="open && (result.data !== null || result.error)" class="qa-card-body">
      <pre v-if="result.error" class="qa-error">{{ result.error }}</pre>
      <pre v-else class="qa-json">{{ JSON.stringify(result.data, null, 2) }}</pre>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import type { QaTest, QaResult } from '@/composables/useQaRunner'

defineProps<{ test: QaTest; result: QaResult }>()
defineEmits<{ run: [] }>()

const STATUS_LABELS: Record<string, string> = {
  idle: '—',
  running: '…',
  pass: 'pass',
  fail: 'fail',
}

const open = ref(false)
function toggle() {
  open.value = !open.value
}
</script>
