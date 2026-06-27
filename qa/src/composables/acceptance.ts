import { reactive, ref, computed } from 'vue'
import { createInjectionState } from '@vueuse/core'

export type TestStatus = 'idle' | 'running' | 'pass' | 'fail'

export interface AcceptanceTest {
  id: string
  group: string
  label: string
  method: 'GET' | 'POST' | 'PUT' | 'DELETE'
  endpoint: () => string
  run: () => Promise<unknown>
  assert?: (result: unknown) => string | null
}

export interface AcceptanceResult {
  status: TestStatus
  data: unknown
  error: string | null
  durationMs: number
}

const IDLE: AcceptanceResult = { status: 'idle', data: null, error: null, durationMs: 0 }

const [provideAcceptance, useAcceptanceInner] = createInjectionState(() => {
  const tests = reactive(new Map<string, AcceptanceTest>())
  const results = reactive<Record<string, AcceptanceResult>>({})
  const allRunning = ref(false)

  function register(test: AcceptanceTest) {
    tests.set(test.id, test)
  }

  function unregister(id: string) {
    tests.delete(id)
    delete results[id]
  }

  function getResult(id: string): AcceptanceResult {
    return results[id] ?? IDLE
  }

  async function runTest(test: AcceptanceTest) {
    results[test.id] = { status: 'running', data: null, error: null, durationMs: 0 }
    const t0 = Date.now()
    try {
      const data = await test.run()
      const durationMs = Date.now() - t0
      const fail = test.assert?.(data) ?? null
      results[test.id] = { status: fail ? 'fail' : 'pass', data, error: fail, durationMs }
    } catch (e) {
      results[test.id] = {
        status: 'fail',
        data: null,
        error: e instanceof Error ? e.message : String(e),
        durationMs: Date.now() - t0,
      }
    }
  }

  async function runGroup(group: string) {
    for (const test of tests.values()) {
      if (test.group === group) await runTest(test)
    }
  }

  async function runAll() {
    allRunning.value = true
    for (const test of tests.values()) await runTest(test)
    allRunning.value = false
  }

  const summary = computed(() => {
    const vals = Object.values(results)
    return {
      total: tests.size,
      pass: vals.filter(r => r.status === 'pass').length,
      fail: vals.filter(r => r.status === 'fail').length,
    }
  })

  return { results, allRunning, register, unregister, getResult, runTest, runGroup, runAll, summary }
})

export function useAcceptanceState() {
  const state = useAcceptanceInner()
  if (!state) throw new Error('useAcceptanceState must be used within AcceptanceRoot')
  return state
}

export { provideAcceptance }
