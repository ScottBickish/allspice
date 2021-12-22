using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using allspice.Models;
using allspice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace allspice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
     private readonly RecipesService _rs;
    private readonly AccountService _acctService;
     private readonly IngredientsService _ins;
    public RecipesController(RecipesService rs, AccountService acctService, IngredientsService ins)
    {
      _rs = rs;
      _acctService = acctService;
      _ins = ins;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Recipe>> Get()
    {
      try
      {
           var Recipes = _rs.Get();
           return Ok(Recipes);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Recipe> Get(int id)
    {
      try
      {
           var Recipe = _rs.Get(id);
           return Ok(Recipe);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newrecipe)
    {
      try
      {
              Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
           newrecipe.CreatorId = userInfo?.Id;
           Recipe recipe =_rs.Create(newrecipe);
           return Ok(recipe);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public ActionResult<Recipe> Edit([FromBody] Recipe updaterecipe, int id)
    {
      try
      {
           updaterecipe.Id = id;
           Recipe recipe = _rs.Edit(updaterecipe);
           return Ok(recipe);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public ActionResult<string> Remove(int id)
    {
      try
      {
           _rs.Remove(id);
           return Ok("you have removed this recipe");
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    //  [HttpGet("{id}")]
    // public ActionResult<Ingredient> GetIngredientByRecipeId(int id)
    // {
    //   try
    //   {
    //        var Ingredients = _ins.GetIngredientByRecipeId(id);
    //        return Ok(Ingredients);
    //   }
    //   catch (Exception e)
    //   {
          
    //       return BadRequest(e.Message);
    //   }
    // }

  }
}