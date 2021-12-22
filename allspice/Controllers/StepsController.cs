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
  public class StepsController : ControllerBase
  {
    private readonly StepsService _ss;
    private readonly AccountService _acctService;
     public StepsController(StepsService ss, AccountService acctService)
    {
      _ss = ss;
      _acctService = acctService;
    }
    [HttpGet("{recipeId}")]
    public ActionResult<IEnumerable<Step>> GetStepByRecipeId(int recipeId)
    {
      try
      {
           var Steps = _ss.GetStepByRecipeId(recipeId);
           return Ok(Steps);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Step>> Create([FromBody] Step newstep)
    {
      try
      {
              Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
           newstep.CreatorId = userInfo?.Id;
           Step step =_ss.Create(newstep);
           return Ok(step);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
     [HttpPut("{id}")]
    public ActionResult<Step> Edit([FromBody] Step updatedstep, int id)
    {
      try
      {
           updatedstep.Id = id;
           Step step = _ss.Edit(updatedstep);
           return Ok(step);
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
         _ss.Remove(id);
         return Ok("you have deleted this step hope you wrote it down");
    }
    catch (Exception e)
    {
        
        return BadRequest(e.Message);
    }
  }
  
  }
}
  