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
    // debugger
    AppState.activeSteps = AppState.activeSteps.push(res.data)
  }
  async removeStep(id){
    await api.delete(`api/Steps/${id}`)
    AppState.activeSteps = AppState.activeSteps.filter(s => s.id !== AppState.activeSteps.id)
  }

}

export const stepsService = new StepsService()