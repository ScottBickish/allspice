import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { recipesService } from "./RecipesService"

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async favorite(favorite){
    const res = await api.post('/Account/favorites', favorite)
    AppState.favorites = [...AppState.favorites, res.data]
  }
  async unfavorite(id){
    await api.delete(`/Account/favorites/${id}`)
    AppState.favorites = AppState.favorites.filter(f => f.id !== id)
    // this.getFavorites()
    await recipesService.getAllRecipes()
  }
  async sortFavorites(){
    const res = await api.get('/Account/favorites')
    AppState.favorites = res.data
  }
}

export const accountService = new AccountService()
