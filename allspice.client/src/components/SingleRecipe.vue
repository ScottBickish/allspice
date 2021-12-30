<template>
  <Modal :id="'editRecipeForm' + recipe.id">
    <template #modal-title> Edit Your Delicious Recipe! </template>

    <template #modal-body> <EditRecipeForm :recipe="recipe" /> </template>
  </Modal>
  <div class="d-flex justify-content-between bg-secondary">
    <div
      v-if="!recipe.favorited"
      class="mdi mdi-heart-broken bg-secondary ms-2 selectable"
      @click="favorite(recipe)"
    ></div>

    <div
      v-if="recipe.favorited"
      class="mdi mdi-heart ms-2 text-danger selectable"
      @click="unfavorite(recipe)"
    ></div>
    <div
      class="mdi mdi-pencil selectable"
      data-bs-toggle="modal"
      :data-bs-target="'#editRecipeForm' + recipe.id"
    ></div>
    <div
      class="bg-secondary rounded-top mdi mdi-close selectable me-2"
      @click="removeRecipe(recipe.id)"
    ></div>
  </div>
  <div
    class="bg-secondary rounded-bottom"
    data-bs-toggle="modal"
    data-bs-target="#recipe-modal"
  >
    <div
      class="justify-content-center d-flex selectable"
      @click="setActiveStuff(recipe.id)"
    >
      <img
        :src="recipe.imgUrl"
        class="card-img-top pic mt-2 img-fluid"
        alt="recipe picture didnt load sorry"
      />
    </div>
    <div class="card-body m-3">
      <em>
        <h5 class="card-title">{{ recipe.title }}</h5>
        <h6 class="card-text">{{ recipe.subTitle }}</h6>

        {{ recipe.category }}
      </em>
    </div>
  </div>
  <Modal id="recipe-modal">
    <template #modal-title>Recipe Info</template>
    <template #modal-body> <ActiveRecipeModal /> </template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { ingredientsService } from "../services/IngredientsService"
import { stepsService } from "../services/StepsService"
import { recipesService } from "../services/RecipesService"
import { logger } from "../utils/Logger"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { accountService } from "../services/AccountService"
export default {
  props: {
    recipe: Object
  },
  setup(props) {
    // let favorited = false
    return {
      props,
      // favorited,

      async setActiveStuff(recipeId) {
        try {
          await recipesService.getRecipeById(recipeId)
          await ingredientsService.getIngredientByRecipeId(recipeId)
          await stepsService.getStepByRecipeId(recipeId)

        } catch (error) {
          logger.error(error)
          Pop.toast(error)

        }
      },
      async removeRecipe() {
        try {
          if (await Pop.confirm('Are you sure you want to delete this recipe?')) {
            await recipesService.removeRecipe(props.recipe.id)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      async favorite(recipe) {
        try {
          let favorite = {}
          favorite.recipeId = recipe.id
          recipe.favorited = !recipe.favorited
          await recipesService.editRecipe(recipe)
          await accountService.favorite(favorite)
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      async unfavorite(recipe) {
        try {
          recipe.favorited = !recipe.favorited
          await recipesService.editRecipe(recipe)
          await accountService.unfavorite(recipe.id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      favorited: computed(() => AppState.favorited),
      account: computed(() => AppState.account),
      favorites: computed(() => AppState.favorites),
      // favorited: computed(() => {
      //   if (AppState.account.id) {
      //     let found = AppState.favorites.find(f => f.recipeId === props.recipe.id)
      //     return found ? true : false
      //   }
      //   return false
      // })

    }
  }
}
</script>


<style lang="scss" scoped>
.pic {
  height: 250px;
  width: 320px;
  object-fit: cover;
}
</style>