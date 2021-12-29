import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService{
  async getIngredientByRecipeId(id){
    const res = await api.get(`api/Ingredients/${id}`)
    AppState.activeIngredients = res.data
  }

  async createIngredient(ingredient){
    const res = await api.post('api/Ingredients', ingredient)
    AppState.activeIngredients = [...AppState.activeIngredients, res.data]
  }
  async removeIngredient(id){
    await api.delete(`api/Ingredients/${id}`)
    AppState.activeIngredients = AppState.activeIngredients.filter(i => i.id !== id)
  }

}

export const ingredientsService = new IngredientsService()