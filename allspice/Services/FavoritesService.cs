using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
  public class FavoritesService
  {
    private readonly FavoritesRepository _repo;
    public FavoritesService(FavoritesRepository repo)
    {
      _repo = repo;
    }
    internal List<Favorite> GetFavorites()
    {
      return _repo.GetFavorites();
    }

    internal Favorite Create(Favorite newfavorite)
    {
      return _repo.Create(newfavorite);
    }
    internal Favorite GetById(int id)
    {
      Favorite found = _repo.GetById(id);
      if(found == null){
        throw new Exception("Invalid Id");
      }
      return found; 
    }

    internal void Remove(int id)
    {
       Favorite favorite = GetById(id);
      _repo.Remove(id);
    }
  }
}