<template>
  <header>
    <Navbar />
  </header>
  <main class="yes">
    <router-view />
  </main>
  <footer>
    <div class="back colors text-center p-4">Made By Scott Bickish</div>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
.back {
  background-color: rgba(255, 162, 40, 0.712);
}
.colors {
  color: rgb(195, 255, 213);
}
.yes {
  background-color: rgb(184, 184, 184);
}
</style>
