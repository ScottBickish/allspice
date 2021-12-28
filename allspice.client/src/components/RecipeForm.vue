<template>
  <div class="RecipeForm">
    <form @submit.prevent="createRecipe()">
      <input
        type="text"
        id="title"
        name="title"
        placeholder="Recipe Title..."
        class="form-control m-3"
        required
        v-model="recipe.title"
      />
      <input
        type="text"
        id="subTitle"
        name="subTitle"
        placeholder="Recipe SubTitle..."
        class="form-control m-3"
        required
        v-model="recipe.subTitle"
      />
      <input
        type="text"
        id="category"
        name="category"
        placeholder="Recipe Category..."
        class="form-control m-3"
        required
        v-model="recipe.category"
      />
      <input
        type="text"
        id="imgUrl"
        name="imgUrl"
        placeholder="Recipe Picture..."
        class="form-control m-3"
        required
        v-model="recipe.imgUrl"
      />
      <button class="btn btn-rounded bg-secondary ms-3" type="submit">
        Post
      </button>
    </form>
  </div>
</template>


<script>
import { ref } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { recipesService } from "../services/RecipesService"
import { Modal } from "bootstrap"
export default {
  setup() {
    const recipe = ref({})
    return {
      recipe,
      async createRecipe() {
        try {
          await recipesService.createRecipe(recipe.value)
          Modal.getOrCreateInstance(document.getElementById("RecipeForm")).hide();
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
</style>