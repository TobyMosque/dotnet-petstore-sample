<template>
  <div class="qa-card" :class="`qa-card--${result.status}`">
    <button class="qa-card-trigger" @click="toggle">
      <span class="qa-method">{{ method ?? 'GET' }}</span>
      <span class="qa-endpoint">{{ resolvedEndpoint }}</span>
      <span class="qa-card-right">
        <span v-if="result.durationMs" class="qa-duration">{{ result.durationMs }}ms</span>
        <q-spinner v-if="result.status === 'running'" size="10px" color="info" />
        <span v-else class="qa-status-dot" :class="`qa-status-dot--${result.status}`" />
        <span class="qa-status-text" :class="`qa-status-text--${result.status}`">
          {{ STATUS[result.status] }}
        </span>
        <q-btn
          flat dense round size="xs" icon="play_arrow"
          :disable="result.status === 'running'"
          @click.stop="handleRun"
        />
      </span>
    </button>
    <div class="qa-card-label">{{ label }}</div>
    <div v-if="open && (result.data !== null || result.error)" class="qa-card-body">
      <pre v-if="result.error" class="qa-error">{{ result.error }}</pre>
      <pre v-else class="qa-json">{{ JSON.stringify(result.data, null, 2) }}</pre>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, inject, onMounted, onUnmounted } from 'vue'
import { useAcceptanceState } from '@/composables/acceptance'

const props = defineProps<{
  id: string
  label: string
  method?: 'GET' | 'POST' | 'PUT' | 'DELETE'
  endpoint: string | (() => string)
  run: () => Promise<unknown>
  assert?: (result: unknown) => string | null
}>()

const STATUS: Record<string, string> = { idle: '—', running: '…', pass: 'pass', fail: 'fail' }

const group = inject<string>('acceptance:group', '')
const { register, unregister, getResult, runTest } = useAcceptanceState()

const resolvedEndpoint = computed(() =>
  typeof props.endpoint === 'function' ? props.endpoint() : props.endpoint
)

onMounted(() =>
  register({
    id: props.id,
    group,
    label: props.label,
    method: props.method ?? 'GET',
    endpoint: typeof props.endpoint === 'function' ? props.endpoint : () => props.endpoint as string,
    run: props.run,
    assert: props.assert,
  })
)
onUnmounted(() => unregister(props.id))

const emit = defineEmits<{ success: [data: unknown] }>()

const result = computed(() => getResult(props.id))

watch(result, (cur, prev) => {
  if (cur.status === 'pass' && prev.status !== 'pass') emit('success', cur.data)
})

const open = ref(false)
function toggle() {
  if (result.value.status !== 'idle') open.value = !open.value
}

function handleRun() {
  void runTest({
    id: props.id,
    group,
    label: props.label,
    method: props.method ?? 'GET',
    endpoint: typeof props.endpoint === 'function' ? props.endpoint : () => props.endpoint as string,
    run: props.run,
    assert: props.assert,
  })
}
</script>
