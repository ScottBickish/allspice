using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Services;
using Microsoft.AspNetCore.Mvc;

namespace allspice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
     private readonly RecipesService _rs;
    private readonly AccountService _acctService;
    public RecipesController(RecipesService rs, AccountService acctService)
    {
      _rs = rs;
      _acctService = acctService;
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
  }
}