<template>
  <form @submit.prevent="search" class="bg-white rounded elevation-1">
    <div class="form-group d-flex align-items-center">
      <input
        v-model="query"
        type="text"
        name="search"
        class="form-control bg-white border-0"
        placeholder="Search Recipes By Category..."
      />
      <button class="btn px-2 py-0" type="submit">
        <i class="mdi mdi-magnify f-18"></i>
      </button>
    </div>
  </form>
  <div class="ms-4 mt-4">
    <span>Sort By Favorites!</span>
    <input
      type="checkbox"
      class="ms-3"
      v-model="sortfavs"
      @click="sortFavorites"
    />
  </div>

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

  <div class="row container fluid" v-if="recipes.category === query">
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
    const query = ref("")
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
      query,
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
      },
      async search() {
        try {
          await recipesService.search(query.value.toLowerCase())
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