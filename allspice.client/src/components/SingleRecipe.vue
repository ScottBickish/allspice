<template>
  <div
    @click="setActiveStuff(recipe.id)"
    class="card selectable"
    data-bs-toggle="modal"
    data-bs-target="#recipe-modal"
  >
    <Modal id="recipe-modal">
      <template #modal-title>Recipe</template>
      <template #modal-body> </template>
    </Modal>
    <div class="justify-content-center d-flex">
      <img
        :src="recipe.imgUrl"
        class="card-img-top pic mt-2 img-fluid"
        alt="recipe picture didnt load sorry"
      />
    </div>
    <div class="card-body m-3">
      <h5 class="card-title">{{ recipe.title }}</h5>
      <p class="card-text">{{ recipe.subTitle }}</p>
      {{ recipe.category }}
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { ingredientsService } from "../services/IngredientsService"
import { stepsService } from "../services/StepsService"
import { recipesService } from "../services/RecipesService"
import { logger } from "../utils/Logger"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
export default {
  props: {
    recipe: Object
  },
  setup() {
    return {
      async getRecipe(id) {
        try {
          await recipesService.getRecipe(id)
        } catch (error) {
          logger.log(error)
        }
      },
      async setActiveStuff(recipeId) {
        try {
          await recipesService.getRecipeById(recipeId)
          await ingredientsService.getIngredientByRecipeId(recipeId)
          await stepsService.getStepByRecipeId(recipeId)

        } catch (error) {
          logger.error(error)
          Pop.toast(error)

        }
      }

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