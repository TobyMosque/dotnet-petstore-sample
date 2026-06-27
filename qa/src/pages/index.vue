<template>
  <q-layout view="hHh lpR fFf">
    <q-header>
      <q-toolbar>
        <q-toolbar-title class="qa-layout-title">PetStore QA</q-toolbar-title>
        <span class="qa-layout-url">{{ apiUrl }}</span>
      </q-toolbar>
    </q-header>
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script setup lang="ts">
const apiUrl = import.meta.env.API_URL ?? 'http://localhost:5000'
</script>
