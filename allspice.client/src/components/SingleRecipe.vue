<template>
  <div
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
        class="card-img-top pic mt-2"
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
import { recipesService } from "../services/RecipesService"
import { logger } from "../utils/Logger"
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