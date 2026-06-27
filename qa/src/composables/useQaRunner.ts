import { ref, computed } from 'vue'

export type TestStatus = 'idle' | 'running' | 'pass' | 'fail'

export interface QaTest {
  id: string
  label: string
  method: 'GET' | 'POST' | 'PUT' | 'DELETE'
  endpoint: string
  run: () => Promise<unknown>
  assert?: (result: unknown) => string | null
}

export interface QaResult {
  status: TestStatus
  data: unknown
  error: string | null
  durationMs: number
}

const EMPTY_RESULT: QaResult = { status: 'idle', data: null, error: null, durationMs: 0 }

export function useQaRunner() {
  const results = ref<Record<string, QaResult>>({})
  const allRunning = ref(false)

  function get(id: string): QaResult {
    return results.value[id] ?? EMPTY_RESULT
  }

  async function run(test: QaTest) {
    results.value[test.id] = { status: 'running', data: null, error: null, durationMs: 0 }
    const t0 = Date.now()
    try {
      const data = await test.run()
      const durationMs = Date.now() - t0
      const failReason = test.assert?.(data) ?? null
      results.value[test.id] = {
        status: failReason ? 'fail' : 'pass',
        data,
        error: failReason,
        durationMs
      }
    } catch (e) {
      const durationMs = Date.now() - t0
      results.value[test.id] = {
        status: 'fail',
        data: null,
        error: e instanceof Error ? e.message : String(e),
        durationMs
      }
    }
  }

  async function runAll(tests: QaTest[]) {
    allRunning.value = true
    for (const test of tests) {
      await run(test)
    }
    allRunning.value = false
  }

  const summary = computed(() => {
    const vals = Object.values(results.value)
    return {
      total: vals.length,
      pass: vals.filter(r => r.status === 'pass').length,
      fail: vals.filter(r => r.status === 'fail').length,
    }
  })

  return { results, allRunning, get, run, runAll, summary }
}
