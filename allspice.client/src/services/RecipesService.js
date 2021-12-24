import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class RecipesService{
  async getAllRecipes(){
    const res = await api.get('api/Recipes')
    logger.log(res.data)
    // AppState.recipes = [...AppState.recipes, res.data]
    // AppState.recipes = AppState.recipes.push(res.data)
    AppState.recipes = res.data
  }

}
export const recipesService = new RecipesService()