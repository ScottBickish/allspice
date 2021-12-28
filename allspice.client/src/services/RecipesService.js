import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class RecipesService{
  async getAllRecipes(){
    const res = await api.get('api/Recipes')
    AppState.recipes = res.data
  }
  // NOTE set active
  async getRecipeById(id){
    const res = await api.get(`api/Recipes/${id}`)
    AppState.activeRecipe = res.data
  }

}
export const recipesService = new RecipesService()