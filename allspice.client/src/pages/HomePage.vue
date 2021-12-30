<template>
  <div class="row container fluid">
    <div class="col-md-4 p-2" v-for="recipe in recipes" :key="recipe.id">
      <SingleRecipe :recipe="recipe" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { recipesService } from "../services/RecipesService"
import { AppState } from "../AppState"
import { accountService } from "../services/AccountService"
export default {

  setup() {
    onMounted(async () => {
      try {
        await recipesService.getAllRecipes()
        await accountService.getFavorites()
      } catch (error) {
        logger.error(error)
        Pop.toast(error)
      }
    })
    return {
      name: 'Home',
      recipes: computed(() => AppState.recipes),
      account: computed(() => AppState.account),
      favorites: computed(() => AppState.favorites),

    }
  }

}

</script>

<style scoped lang="scss">
// Modal.getOrCreateInstance(document.getElementById('editGameNight-' + gameNightId)).hide()
</style>