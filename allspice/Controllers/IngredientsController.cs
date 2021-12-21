using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Services;
using Microsoft.AspNetCore.Mvc;

namespace allspice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class IngredientsController : ControllerBase
  {
    private readonly IngredientsService _ins;
    private readonly AccountService _acctService;
     public IngredientsController(IngredientsService ins, AccountService acctService)
    {
      _ins = ins;
      _acctService = acctService;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Ingredient>> Get()
    {
      try
      {
           var Ingredients = _ins.Get();
           return Ok(Ingredients);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
  }
}