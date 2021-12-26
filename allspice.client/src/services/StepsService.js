import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class StepsService{
  async getStepByRecipeId(id){
    const res = await api.get(`api/Steps/${id}`)
    logger.log(res.data, 'steps service')
  }

}

export const stepsService = new StepsService()