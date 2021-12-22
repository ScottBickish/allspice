using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class IngredientsRepository
  {
     private readonly IDbConnection _db;
    public IngredientsRepository(IDbConnection db)
    {
      _db = db;
    }
        internal List<Ingredient> Get()
    {
      string sql = "SELECT * FROM  ingredients;";
      return _db.Query<Ingredient>(sql).ToList();
    }

    internal Ingredient GetById(int id)
    {
      string sql = @"SELECT * FROM ingredients
      WHERE id = @id
      ;";
      return _db.QueryFirstOrDefault<Ingredient>(sql, new{id});
    }

    internal Ingredient Create(Ingredient newingredient)
    {
        string sql = @"INSERT INTO ingredients 
      (id, creatorId, ingredientname, quantity, recipeId)
      VALUE(@id, @creatorId, @ingredientname, @quantity, @recipeId)
      ;SELECT LAST_INSERT_ID();";

      int id = _db.ExecuteScalar<int>(sql, newingredient);
      newingredient.Id = id;
      return newingredient;
    }

    internal Ingredient Edit(Ingredient updatedIngredient)
    {
       string sql = @"
      UPDATE ingredients
      SET
      ingredientname = @IngredientName,
      quantity = @Quantity,
      recipeId = @RecipeId,
      creatorId = @CreatorId
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, updatedIngredient);
      if (rows <= 0)
      {
        throw new Exception("Ingredient was not updated Repository");
      }
      return updatedIngredient;
    }
  }
}