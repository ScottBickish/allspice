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
export default {

  setup() {
    onMounted(async () => {
      try {
        await recipesService.getAllRecipes()
      } catch (error) {
        logger.error(error)
        Pop.toast(error)
      }
    })
    return {
      name: 'Home',
      recipes: computed(() => AppState.recipes),

    }
  }

}

</script>

<style scoped lang="scss">
</style>
