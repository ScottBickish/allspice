<template>
  <div class="Active Recipe Modal row justify-space-evenly">
    <div class="col-md-4">
      <img class="img-fluid pic" :src="activeRecipe.imgUrl" alt="yummy photo" />
      <div class="bg-secondary">
        <h5 class="p-2">{{ activeRecipe.title }}</h5>
        <h5 class="p-2">{{ activeRecipe.subTitle }}</h5>
      </div>
    </div>
    <div class="col-md-4">
      <div class="bg-secondary text-center"><p>Recipe Steps</p></div>
      <div
        v-for="a in activeSteps"
        :key="a.id"
        class="d-flex justify-content-between"
      >
        <!-- FIXME needs recipe id in the body before its sent to the service -->
        <div>
          <p>{{ a.recipeStepOrder }}.) {{ a.body }}</p>
        </div>
        <div
          class="mdi mdi-trash-can selectable text-danger"
          @click="removeStep(ActiveStepId)"
        ></div>
      </div>
      <form @submit.prevent="createStep()">
        <input
          type="number"
          id="recipeStepOrder"
          name="recipeStepOrder"
          placeholder="Step Order..."
          class="form-control my-2"
          required
          v-model="step.recipeStepOrder"
        />
        <input
          type="text"
          name="body"
          id="body"
          placeholder="Step here..."
          class="form-control my-2"
          required
          v-model="step.body"
        />
        <button class="btn btn-rounded bg-secondary" type="submit">Post</button>
      </form>
    </div>
    <div class="col-md-4">
      <div class="bg-secondary text-center">Ingredients</div>
      <div v-for="i in activeIngredients" :key="i.id">
        <p>{{ i.quantity }} of {{ i.ingredientName }}</p>
      </div>
      <form @submit.prevent="createIngredient()">
        <input
          type="text"
          id="quantity"
          name="quantity"
          placeholder="Quantity..."
          class="form-control my-2"
          required
        />
        <input
          type="text"
          name="ingredientName"
          id="ingredientName"
          placeholder="Ingredient..."
          class="form-control my-2"
          required
        />
        <button class="btn btn-rounded bg-secondary" type="submit">Post</button>
      </form>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { stepsService } from "../services/StepsService"
export default {
  setup() {
    let step = ref({ recipeId: AppState.activeRecipe.id })
    let ActiveStepId = computed(() => AppState.activeSteps.find(a => a.id === AppState.activeSteps.id))
    return {
      step,
      ActiveStepId,
      activeRecipe: computed(() => AppState.activeRecipe),
      activeIngredients: computed(() => AppState.activeIngredients),
      activeSteps: computed(() => AppState.activeSteps),
      async createStep() {
        try {
          // debugger
          step.recipeId = AppState.activeRecipe.id
          await stepsService.createStep(step.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      async removeStep(ActiveStepId) {
        try {
          // Id = AppState.activeSteps.id
          if (await Pop.confirm('Are you sure you want to remove this step?')) {
            await stepsService.removeStep(ActiveStepId)
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