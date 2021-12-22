using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
  public class StepsService
  {
     private readonly StepsRepository _repo;
    public StepsService(StepsRepository repo)
    {
      _repo = repo;
    }
    internal IEnumerable<Step> GetStepByRecipeId(int recipeId)
    {
      // return _repo.GetStepByRecipeId(recipeId);
      IEnumerable<Step> found = _repo.GetStepByRecipeId(recipeId);
      if(found == null){
        throw new Exception("Invalid recipeId");
      }
      return found; 
    }

    internal Step Create(Step newstep)
    {
      return _repo.Create(newstep);
    }
    internal Step GetById(int id)
    {
      Step found = _repo.GetById(id);
      if(found == null){
        throw new Exception("Invalid Id");
      }
      return found; 
    }

    internal Step Edit(Step updatedStep)
    {
       Step oldStep = GetById(updatedStep.Id);
      updatedStep.RecipeStepOrder = updatedStep.RecipeStepOrder != null ? updatedStep.RecipeStepOrder : oldStep.RecipeStepOrder;
      updatedStep.Body = updatedStep.Body != null ? updatedStep.Body : oldStep.Body;
      updatedStep.RecipeId = updatedStep.RecipeId != null ? updatedStep.RecipeId : oldStep.RecipeId;
      updatedStep.CreatorId = updatedStep.CreatorId != null ? updatedStep.CreatorId : oldStep.CreatorId;
      return _repo.Edit(updatedStep);
    }

    internal void Remove(int id)
    {
      Step step = GetById(id);
      _repo.Remove(id);
    }
  }
}