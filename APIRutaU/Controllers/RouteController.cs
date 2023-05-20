using APIRutaU.Helpers.Mail;
using APIRutaU.Helpers.Token;
using APIRutaU.Models;
using APIRutaU.Repository.Account;
using APIRutaU.Repository.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIRutaU.Controllers
{
    [EnableCors("PolicyCore")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RouteController : ControllerBase
    {
        private readonly ITokenHelper _Token;
        private readonly IRepositoryRoute _repository;
        private readonly IMailHelper _Mail;
        public RouteController(IRepositoryRoute repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> GetNearbyRoutes([FromBody]GetNearbyRoutesViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.GetNearbyRoutes(model);
            List<object> routes = new List<object>();
            if (peticion.Count == 0)
            {
                return Ok(new
                {
                    Routes = routes
                });

            }
            var response = peticion[0];
            if (response.Id != null)
            {
                foreach (var item in peticion)
                {
                    var route = new
                    {
                        Id = item.Id,
                        NameUser = item.StrName,
                        DateStart = "20",
                        SpaceAvailable = item.SpaceAvailable,
                        LatitudeStart = item.LatitudeStart,
                        LongitudeStart = item.LongitudeStart,
                        LatitudeEnd = item.LatitudeEnd,
                        LongitudeEnd = item.LongitudeEnd
                    };
                    routes.Add(route);
                }
            }
            else
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Cod = response.Cod
                });
            }
            return Ok(new
            {
                Routes = routes
            });
        }

    }
}
