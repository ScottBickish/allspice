using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class FavoritesRepository
  {
    private readonly IDbConnection _db;
    public FavoritesRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Favorite> GetFavorites()
    {
      string sql = "SELECT * FROM  favorites;";
      return _db.Query<Favorite>(sql).ToList();
    }

    internal Favorite Create(Favorite newfavorite)
    {
       string sql = @"INSERT INTO favorites 
      (accountId, recipeId, id)
      VALUE(@accountId, @recipeId, @id)
      ;SELECT LAST_INSERT_ID();";

      int id = _db.ExecuteScalar<int>(sql, newfavorite);
      newfavorite.Id = id;
      return newfavorite;
    }

    internal void Remove(int recipeId)
    {
      string sql = @"
      DELETE FROM favorites
      WHERE recipeId = @recipeId
      ;";
      int rows = _db.Execute(sql, new { recipeId });
      if (rows <= 0)
      {
        throw new Exception("invalid Id");
      }
    }

    internal Favorite GetById(int recipeId)
    {
      string sql = @"SELECT * FROM favorites
      WHERE recipeId = @recipeId
      ;";
      return _db.QueryFirstOrDefault<Favorite>(sql, new{recipeId});
    }
    
  }
}