<template>
  <div class="ActiveSteps d-flex">
    <div contenteditable @blur="editStepsOrder">
      <span>{{ a.recipeStepOrder }}</span>
    </div>
    <span>.) </span>
    <div contenteditable @blur="editSteps">
      <p class="ms-1">{{ a.body }}</p>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { stepsService } from "../services/StepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
export default {
  props: {
    a: Object,
    activeRecipe: Object
  },
  setup(props) {

    return {
      props,
      async editSteps(event) {
        try {
          logger.log(event)
          let stepId = props.a.id
          let data = event.target.innerText
          let rule = { body: data, recipeId: props.activeRecipe.id }
          await stepsService.editSteps(stepId, rule)
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      async editStepsOrder(event) {
        try {
          let stepId = props.a.id
          let data = event.target.innerText
          let rule = { recipeId: props.activeRecipe.id, recipeStepOrder: data }
          await stepsService.editStepsOrder(stepId, rule)
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