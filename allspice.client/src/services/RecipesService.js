import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class RecipesService{
  async getAllRecipes(){
    const res = await api.get('api/Recipes')
    AppState.recipes = res.data
  }
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
    AppState.recipes = AppState.recipes.filter(r => r.id !== id)
  }
  async editRecipe(recipe){
    const res = await api.put(`api/Recipes/${recipe.id}`, recipe)
    const newRecipe = res.data
    
    const index = AppState.recipes.findIndex(r => r.id === newRecipe.id)
    if(index === -1){
      AppState.recipes.push(newRecipe)
      return
    }
    AppState.recipes.splice(index, 1, newRecipe)
  }
  async search(query){
    const res = await api.get(`api/Recipes/recipecategories/${query}`)
    logger.log(res.data)
    AppState.recipes = AppState.recipes.filter(r => r.category === res.data.category)
    // AppState.queryRecipes = res.data
    

    
  }
  

}
export const recipesService = new RecipesService()