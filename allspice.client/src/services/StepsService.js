import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class StepsService{
  async getStepByRecipeId(id){
    const res = await api.get(`api/Steps/${id}`)
    AppState.activeSteps = res.data
  }
  async createStep(step){
    
    const res = await api.post('api/Steps', step)
    AppState.activeSteps = [...AppState.activeSteps, res.data]
  }
  async removeStep(id){
    await api.delete(`api/Steps/${id}`)
    AppState.activeSteps = AppState.activeSteps.filter(s => s.id !== id)
  }
  async editSteps(stepId, step){
    
    const res = await api.put(`api/Steps/${stepId}`, step)
    logger.log(res.data)
    const newStep = res.data
    // delete res.data.recipeStepOrder
    
    const index = AppState.activeSteps.findIndex(r => r.id === newStep.id)
   
    AppState.activeSteps.splice(index, 1, newStep)
  }
  async editStepsOrder(stepId, step){
    const res = await api.put(`api/Steps/${stepId}`, step)
    logger.log(res.data)
    const newStepOrder = res.data
    // delete res.data.recipeStepOrder
    
    const index = AppState.activeSteps.findIndex(r => r.id === newStepOrder.id)
   
    AppState.activeSteps.splice(index, 1, newStepOrder)
  }

}

export const stepsService = new StepsService()