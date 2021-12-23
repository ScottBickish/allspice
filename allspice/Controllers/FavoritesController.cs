// using System;
// using System.Collections.Generic;
// using allspice.Models;
// using allspice.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace allspice.Controllers
// {
//   [ApiController]
//   [Route("api/[controller]")]
//   public class FavoritesController : ControllerBase
//   {
//     private readonly FavoritesService _fs;
//     private readonly AccountService _acctService;
//      public FavoritesController(FavoritesService fs, AccountService acctService)
//     {
//       _fs = fs;
//       _acctService = acctService;
//     }
//     [HttpGet]
//     public ActionResult<IEnumerable<Favorite>> Get()
//     {
//       try
//       {
//            var Favorites = _fs.Get();
//            return Ok(Favorites);
//       }
//       catch (Exception e)
//       {
          
//           return BadRequest(e.Message);
//       }
//     }
//   }
// }