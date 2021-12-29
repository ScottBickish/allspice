<template>
  <div class="ActiveSteps" contenteditable @blur="editSteps">
    <div>
      <p>{{ a.recipeStepOrder }}.) {{ a.body }}</p>
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
    a: Object
  },
  setup(props) {
    let activeRecipeId = computed(() => AppState.activeRecipe.id)
    const editable = ref({ recipeId: activeRecipeId })
    return {
      editable,
      props,
      async editSteps(event) {
        try {
          // logger.log(event)
          let stepId = props.a.id
          let data = event.target.innerText
          let rule = { body: data, recipeStepOrder: data, recipeId: editable.value }
          await stepsService.editSteps(stepId, rule)
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