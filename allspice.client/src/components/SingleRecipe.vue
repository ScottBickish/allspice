<template>
  <div
    class="bg-secondary rounded-top mdi mdi-close text-end selectable"
    @click="removeRecipe(recipe.id)"
  ></div>
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
      <h5 class="card-title">{{ recipe.title }}</h5>
      <p class="card-text">{{ recipe.subTitle }}</p>
      {{ recipe.category }}
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
export default {
  props: {
    recipe: Object
  },
  setup(props) {
    return {
      props,

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
          // id = AppState.recipes.find(r => r.id === id)
          if (await Pop.confirm('Are you sure you want to delete this recipe?')) {
            await recipesService.removeRecipe(props.recipe.id)
          }
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