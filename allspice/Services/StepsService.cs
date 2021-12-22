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
  }
}