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
  async createRecipe(recipe){
    const res = await api.post('api/Recipes', recipe)
    logger.log(res.data, 'Posted recipe from modal')
    AppState.recipes = AppState.recipes.push(res.data)
  }
  
  async removeRecipe(id){
    await api.delete(`api/Recipes/${id}`)
    this.getAllRecipes()
  }

}
export const recipesService = new RecipesService()