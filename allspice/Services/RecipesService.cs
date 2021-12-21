using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _repo;
    public RecipesService(RecipesRepository repo)
    {
      _repo = repo;
    }
    internal List<Recipe> Get()
        {
          return _repo.Get();
        }

    internal Recipe Get(int id)
    {
      Recipe found = _repo.Get(id);
      if(found == null){
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Recipe Create(Recipe newrecipe)
    {
      return _repo.Create(newrecipe);
    }

    internal Recipe Edit(Recipe updatedRecipe)
    {
      Recipe oldRecipe = Get(updatedRecipe.Id);
      updatedRecipe.Title = updatedRecipe.Title != null ? updatedRecipe.Title : oldRecipe.Title;
      updatedRecipe.SubTitle = updatedRecipe.SubTitle != null ? updatedRecipe.SubTitle : oldRecipe.SubTitle;
      updatedRecipe.Category = updatedRecipe.Category != null ? updatedRecipe.Category : oldRecipe.Category;
      updatedRecipe.CreatorId = updatedRecipe.CreatorId != null ? updatedRecipe.CreatorId : oldRecipe.CreatorId;
      return _repo.Edit(updatedRecipe);
    }

    internal void Remove(int id)
    {
         
      Recipe recipe = Get(id);
      _repo.Remove(id);
    
    }
  }
}