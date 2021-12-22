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
    [HttpGet("{id}")]
    public ActionResult<Ingredient> GetById(int id)
    {
      try
      {
           var Ingredients = _ins.GetById(id);
           return Ok(Ingredients);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
     public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient newingredient)
    {
      try
      {
              Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
           newingredient.CreatorId = userInfo?.Id;
           Ingredient ingredient =_ins.Create(newingredient);
           return Ok(ingredient);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
     [HttpPut("{id}")]
    public ActionResult<Ingredient> Edit([FromBody] Ingredient updatedingredient, int id)
    {
      try
      {
           updatedingredient.Id = id;
           Ingredient ingredient = _ins.Edit(updatedingredient);
           return Ok(ingredient);
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
            _ins.Remove(id);
           return Ok("you have removed this ingredient");
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }

  }
}