using APIRutaU.Helpers.Token;
using APIRutaU.Models;
using APIRutaU.Repository.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIRutaU.Controllers
{
    [EnableCors("PolicyCore")]
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenHelper _Token;
        private readonly IRepositoryAccount _repository;
        public AccountController(ITokenHelper token, IRepositoryAccount repository)
        {
            _Token = token;
            _repository = repository;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var peticion = await _repository.Login(model);
            var response = peticion[0]; //Obtengo el primer elemento de la respuesta
            if (response.Id != null) // Si la validación fue exitosa, genero un token y lo envío en la respuesta
            {
                return Ok(new
                {
                    Name = response.SrtName,
                    Token = _Token.CreateToken(new[]{new Claim("User_Id", response.Id.ToString())}, TimeSpan.FromMinutes(30))
                });
            }
            else // Si la validación no fue exitosa, retorno un mensaje de error
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Cod = response.Cod
                });
            }
        }
    }
}