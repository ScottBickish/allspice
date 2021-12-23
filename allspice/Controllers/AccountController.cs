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
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly FavoritesService _fs;

        public AccountController(AccountService accountService, FavoritesService fs)
        {
            _accountService = accountService;
            _fs = fs;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
         [HttpGet("{favorites}")]
    public ActionResult<IEnumerable<Favorite>> GetFavorites()
    {
      try
      {
           var Favorites = _fs.GetFavorites();
           return Ok(Favorites);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    [HttpPost("{favorites}")]
     [Authorize]
     public async Task<ActionResult<Favorite>> Create([FromBody] Favorite newfavorite)
    {
      try
      {
              Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
           newfavorite.AccountId = userInfo?.Id;
           Favorite favorite =_fs.Create(newfavorite);
           return Ok(favorite);
      }
      catch (Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    [HttpDelete("{favorites}/{id}")]
    public ActionResult<string> Remove(int id)
    {
        try
        {
             _fs.Remove(id);
             return Ok("you have unfavorited this recipe.");
        }
        catch (Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }
    }


}