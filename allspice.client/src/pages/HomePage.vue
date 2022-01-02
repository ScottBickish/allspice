<template>
  <span class="ms-4">Sort By Favorites!</span>
  <input
    type="checkbox"
    class="ms-2"
    v-model="sortfavs"
    @click="sortFavorites"
  />

  <div class="row container fluid" v-if="sortfavs">
    <div
      class="col-md-4 p-5"
      v-for="recipe in favoritedRecipes"
      :key="recipe.id"
    >
      <SingleRecipe :recipe="recipe" />
    </div>
  </div>

  <div class="row container fluid" v-else>
    <div class="col-md-4 p-5" v-for="recipe in recipes" :key="recipe.id">
      <SingleRecipe :recipe="recipe" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { recipesService } from "../services/RecipesService"
import { AppState } from "../AppState"
import { accountService } from "../services/AccountService"
export default {

  setup() {
    let sortfavs = ref(false)
    onMounted(async () => {
      try {
        await recipesService.getAllRecipes()
        // await accountService.getFavorites()
      } catch (error) {
        logger.error(error)
        Pop.toast(error)
      }
    })
    return {
      sortfavs,
      name: 'Home',
      recipes: computed(() => AppState.recipes),
      account: computed(() => AppState.account),
      favorites: computed(() => AppState.favorites),
      favoritedRecipes: computed(() => AppState.recipes.filter(r => r.favorited === true)),
      async sortFavorites() {
        try {
          await accountService.sortFavorites()
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      }

    }
  }

}

</script>

<style scoped lang="scss">
</style>