import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService{
  async getIngredientByRecipeId(id){
    const res = await api.get(`api/Ingredients/${id}`)
    AppState.activeIngredients = res.data
  }

}

export const ingredientsService = new IngredientsService()