<template>
  <div class="Active Ingredients d-flex">
    <div contenteditable @blur="editquantity">
      <span>{{ i.quantity }}</span>
    </div>
    <span class="ms-1"> of </span>
    <div contenteditable @blur="editingredientName">
      <p class="ms-1">{{ i.ingredientName }}</p>
    </div>
  </div>
</template>


<script>
import { ingredientsService } from "../services/IngredientsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"

export default {
  props: {
    i: Object,
    activeRecipe: Object
  },
  setup(props) {
    return {
      props,
      async editquantity(event) {
        try {
          let ingredientId = props.i.id
          let data = event.target.innerText
          let rule = { quantity: data, recipeId: props.activeRecipe.id }
          await ingredientsService.editquantity(ingredientId, rule)
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      async editingredientName(event) {
        try {
          let ingredientId = props.i.id
          let data = event.target.innerText
          let rule = { ingredientName: data, recipeId: props.activeRecipe.id }
          await ingredientsService.editingredientName(ingredientId, rule)
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },

    }
  }
}
</script>


<style lang="scss" scoped>
</style>