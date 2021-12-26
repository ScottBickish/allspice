import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService{
  async getIngredientByRecipeId(id){
    const res = await api.get(`api/Ingredients/${id}`)
    logger.log(res.data, "ingredient service")
  }

}

export const ingredientsService = new IngredientsService()