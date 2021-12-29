<template>
  <div class="d-flex justify-content-between bg-secondary">
    <div
      class="mdi mdi-heart bg-secondary text-danger ms-2 selectable"
      @click="favorite(recipe.id)"
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
          if (await Pop.confirm('Are you sure you want to delete this recipe?')) {
            await recipesService.removeRecipe(props.recipe.id)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      async favorite(id) {
        try {
          await accountService.favorite(id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },

      account: computed(() => AppState.account)
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