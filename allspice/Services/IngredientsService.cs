using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
  public class IngredientsService
  {
     private readonly IngredientsRepository _repo;
    public IngredientsService(IngredientsRepository repo)
    {
      _repo = repo;
    }
    internal List<Ingredient> Get()
        {
          return _repo.Get();
        }

    internal Ingredient GetById(int id)
    {
      Ingredient found = _repo.GetById(id);
      if(found == null){
        throw new Exception("Invalid Id");
      }
      return found; 
    }

    internal Ingredient Create(Ingredient newingredient)
    {
      return _repo.Create(newingredient);
    }

    internal Ingredient Edit(Ingredient updatedIngredient)
    {
       Ingredient oldIngredient = GetById(updatedIngredient.Id);
      updatedIngredient.IngredientName = updatedIngredient.IngredientName != null ? updatedIngredient.IngredientName : oldIngredient.IngredientName;
      updatedIngredient.Quantity = updatedIngredient.Quantity != null ? updatedIngredient.Quantity : oldIngredient.Quantity;
      updatedIngredient.RecipeId = updatedIngredient.RecipeId != null ? updatedIngredient.RecipeId : oldIngredient.RecipeId;
      updatedIngredient.CreatorId = updatedIngredient.CreatorId != null ? updatedIngredient.CreatorId : oldIngredient.CreatorId;
      return _repo.Edit(updatedIngredient);
    }

    internal void Remove(int id)
    {
      Ingredient ingredient = GetById(id);
      _repo.Remove(id);
    }

    internal IEnumerable<Ingredient> GetIngredientByRecipeId(int recipeId)
    {
      // return _repo.GetIngredientByRecipeId(recipeId);
      IEnumerable<Ingredient> found = _repo.GetIngredientByRecipeId(recipeId);
      if(found == null){
        throw new Exception("Invalid recipeId");
      }
      return found; 
    }
  }
}