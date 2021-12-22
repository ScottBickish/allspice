using System.Collections.Generic;
using System.Data;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class StepsRepository
  {
     private readonly IDbConnection _db;
    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal  IEnumerable<Step> GetStepByRecipeId(int recipeId)
    {
        string sql = @"SELECT * FROM steps
      WHERE recipeId = @recipeId
      ;";
      
      return _db.Query<Step>(sql, new {recipeId});
    }

    internal Step Create(Step newstep)
    {
      string sql = @"INSERT INTO steps 
      (id, recipesteporder, body, recipeId, creatorId)
      VALUE(@id, @recipesteporder, @body, @recipeId, @creatorId)
      ;SELECT LAST_INSERT_ID();";

      int id = _db.ExecuteScalar<int>(sql, newstep);
      newstep.Id = id;
      return newstep;
    }
  }
}