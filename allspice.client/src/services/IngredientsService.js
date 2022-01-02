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
  async editquantity(ingredientId, ingredient){
    const res = await api.put(`api/Ingredients/${ingredientId}`, ingredient)
    const newIngredient = res.data
    
    const index = AppState.activeIngredients.findIndex(i => i.id === newIngredient.id)
   
    AppState.activeIngredients.splice(index, 1, newIngredient)
  }

  async editingredientName(ingredientId, ingredient){
    const res = await api.put(`api/Ingredients/${ingredientId}`, ingredient)
    const newIngredient = res.data
    
    const index = AppState.activeIngredients.findIndex(i => i.id === newIngredient.id)
   
    AppState.activeIngredients.splice(index, 1, newIngredient)
  }
}

export const ingredientsService = new IngredientsService()