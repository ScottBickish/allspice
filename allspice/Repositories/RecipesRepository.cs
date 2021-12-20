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
  }
}