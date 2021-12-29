<template>
  <div class="EditRecipeForm" id="EditRecipeForm">
    <form @submit.prevent="handleSubmit">
      <input
        type="text"
        id="title"
        name="title"
        placeholder="Recipe Title..."
        class="form-control m-3"
        required
        v-model="editable.title"
      />
      <input
        type="text"
        id="subTitle"
        name="subTitle"
        placeholder="Recipe SubTitle..."
        class="form-control m-3"
        required
        v-model="editable.subTitle"
      />
      <input
        type="text"
        id="category"
        name="category"
        placeholder="Recipe Category..."
        class="form-control m-3"
        required
        v-model="editable.category"
      />
      <input
        type="text"
        id="imgUrl"
        name="imgUrl"
        placeholder="Recipe Picture..."
        class="form-control m-3"
        required
        v-model="editable.imgUrl"
      />
      <button class="btn btn-rounded bg-secondary ms-3" type="submit">
        Save
      </button>
    </form>
  </div>
</template>


<script>
import { ref } from "@vue/reactivity"
import { watchEffect } from "@vue/runtime-core"
import { recipesService } from "../services/RecipesService"
import { Modal } from "bootstrap"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
export default {
  props: {
    recipe: {
      type: Object,
    }
  },
  setup(props) {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...props.recipe }
    })
    return {
      props,
      editable,
      async handleSubmit() {
        try {
          await recipesService.editRecipe(editable.value)
          Modal.getOrCreateInstance(document.getElementById('EditRecipeForm')).hide()
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