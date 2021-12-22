using System;
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

    internal Step GetById(int id)
    {
      string sql = @"SELECT * FROM steps
      WHERE id = @id
      ;";
      return _db.QueryFirstOrDefault<Step>(sql, new{id});
    }

    internal Step Edit(Step updatedStep)
    {
       string sql = @"
      UPDATE steps
      SET
      recipesteporder = @RecipeStepOrder,
      body = @Body,
      recipeId = @RecipeId,
      creatorId = @CreatorId
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, updatedStep);
      if (rows <= 0)
      {
        throw new Exception("Step was not updated Repository");
      }
      return updatedStep;
    }

    internal void Remove(int id)
    {
      string sql = @"
      DELETE FROM steps
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, new { id });
      if (rows <= 0)
      {
        throw new Exception("invalid Id");
      }
    }
  }
}