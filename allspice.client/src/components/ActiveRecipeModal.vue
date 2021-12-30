<template>
  <div class="Active Recipe Modal row justify-space-evenly">
    <div class="col-md-4 color rounded">
      <img
        class="img-fluid pic rounded"
        :src="activeRecipe.imgUrl"
        alt="yummy photo"
      />
      <div class="back rounded">
        <em>
          <h5 class="p-2">{{ activeRecipe.title }}</h5>
          <h5 class="p-2">{{ activeRecipe.subTitle }}</h5>
        </em>
      </div>
    </div>
    <div class="col-md-4 color rounded">
      <div class="back text-center rounded"><p>Recipe Steps</p></div>
      <div
        v-for="a in activeSteps"
        :key="a.id"
        class="d-flex justify-content-between"
      >
        <div>
          <ActiveSteps :a="a" :activeRecipe="activeRecipe" />
        </div>
        <!-- <div
          contenteditable
          @blur="editSteps"
          class="d-flex justify-content-between"
        >
          <p>{{ a.recipeStepOrder }}.) {{ a.body }}</p>
        </div> -->
        <div
          class="mdi mdi-trash-can selectable text-danger"
          @click="removeStep(a.id)"
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
        <button class="btn btn-rounded back" type="submit">Post</button>
      </form>
    </div>
    <div class="col-md-4 color rounded">
      <div class="back text-center rounded">Ingredients</div>
      <div
        v-for="i in activeIngredients"
        :key="i.id"
        class="d-flex justify-content-between"
      >
        <div>
          <p>{{ i.quantity }} of {{ i.ingredientName }}</p>
        </div>
        <div
          class="mdi mdi-trash-can selectable text-danger"
          @click="removeIngredient(i.id)"
        ></div>
      </div>
      <form @submit.prevent="createIngredient()">
        <input
          type="text"
          id="quantity"
          name="quantity"
          placeholder="Quantity..."
          class="form-control my-2"
          required
          v-model="ingredient.quantity"
        />
        <input
          type="text"
          name="ingredientName"
          id="ingredientName"
          placeholder="Ingredient..."
          class="form-control my-2"
          required
          v-model="ingredient.ingredientName"
        />
        <button class="btn btn-rounded back" type="submit">Post</button>
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
import { ingredientsService } from "../services/IngredientsService"
export default {
  setup() {
    let activeRecipeId = computed(() => AppState.activeRecipe.id)
    const step = ref({ recipeId: activeRecipeId })
    const ingredient = ref({ recipeId: activeRecipeId })
    const editable = ref({ recipeId: activeRecipeId })
    return {
      step,
      ingredient,
      editable,
      activeRecipe: computed(() => AppState.activeRecipe),
      activeIngredients: computed(() => AppState.activeIngredients),
      activeSteps: computed(() => AppState.activeSteps.sort((a, b) => {
        return a.recipeStepOrder - b.recipeStepOrder
      })),
      async createStep() {
        try {
          await stepsService.createStep(step.value)
          step.value = { recipeId: activeRecipeId }
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      async removeStep(id) {
        try {
          if (await Pop.confirm('Are you sure you want to remove this step?')) {
            await stepsService.removeStep(id)
          }

        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      async createIngredient() {
        try {
          await ingredientsService.createIngredient(ingredient.value)
          ingredient.value = { recipeId: activeRecipeId }
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      async removeIngredient(id) {
        try {
          if (await Pop.confirm('Are you sure you want to remove this ingredient?')) {
            await ingredientsService.removeIngredient(id)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      // async editSteps(event) {
      //   try {
      //     // logger.log(event)
      //     let stepId = event.id
      //     let data = event.target.innerText
      //     let rule = { recipeStepOrder: data, body: data }
      //     await stepsService.editSteps(stepId, rule)
      //   } catch (error) {
      //     logger.error(error)
      //     Pop.toast(error)
      //   }
      // }
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
.color {
  background-color: rgb(195, 255, 213);
}
.back {
  background-color: rgba(255, 162, 40, 0.712);
}
</style>