using System.Collections.Generic;
using System.Data;
using System.Linq;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class RecipesRepository
  {
       private readonly IDbConnection _db;
    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }
        internal List<Recipe> Get()
    {
      string sql = "SELECT * FROM  recipes;";
      return _db.Query<Recipe>(sql).ToList();
    }
    internal Recipe Get(int id)
    {
      string sql = @"SELECT * FROM recipes
      WHERE id = @id
      ;";
      return _db.QueryFirstOrDefault<Recipe>(sql, new{id});
    }

    internal Recipe Create(Recipe newrecipe)
    {
       string sql = @"INSERT INTO recipes 
      (title, subtitle, category, creatorId)
      VALUE(@title, @subtitle, @category, @creatorId)
      ;SELECT LAST_INSERT_ID();";

      int id = _db.ExecuteScalar<int>(sql, newrecipe);
      newrecipe.Id = id;
      return newrecipe;
    }
  }
}