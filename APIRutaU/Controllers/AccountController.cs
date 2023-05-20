using APIRutaU.Helpers.Mail;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenHelper _Token;
        private readonly IRepositoryAccount _repository;
        private readonly IMailHelper _Mail;
        public AccountController(ITokenHelper token, IRepositoryAccount repository, IMailHelper mail)
        {
            _Token = token;
            _repository = repository;
            _Mail = mail;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var peticion = await _repository.Login(model);
            var response = peticion[0]; //Obtengo el primer elemento de la respuesta
            if (response.Id != null) // Si la validación fue exitosa, genero un token y lo envío en la respuesta
            {
                return Ok(new
                {
                    Name = response.StrName,
                    Token = _Token.CreateToken(new[] { new Claim("User_Id", response.Id.ToString()) }, TimeSpan.FromMinutes(30))
                });
            }
            else // Si la validación no fue exitosa, retorno un mensaje de error
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Codigo = response.Codigo
                });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ValidateUser()
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            ValidateUserByIdViewModel model = new()
            {
                User_Id = Guid.Parse(userIdClaim)
            };
            var peticion = await _repository.ValidateUserById(model);
            var response = peticion[0]; //Obtengo el primer elemento de la respuesta
            if (response.Id != null) // Si la validación fue exitosa, genero un token y lo envío en la respuesta
            {
                return Ok(new
                {
                    Name = response.StrName
                });
            }
            else // Si la validación no fue exitosa, retorno un mensaje de error
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Codigo = response.Codigo
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetOTP([FromBody] GetOTPViewModel model)
        {
            var peticion = await _repository.GetOTP(model);
            var response = peticion[0];
            if (response.Codigo != "-1")
            {
                return Ok(await _Mail.SendMail(new string[] { response.Email }, new string[] { }, "Recuperación de contraseña", $"Este es un correo electrónico de prueba y este es tu código{response.Rpta}"));
            }
            else
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Codigo = response.Codigo   
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ValidateOTP([FromBody] ValidateOTPViewModel model)
        {
            var peticion = await _repository.ValidateOTP(model);
            var response = peticion[0];
            return Ok(new
            {
                Rpta = response.Rpta,
                Codigo = response.Codigo
            });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordByOTP([FromBody] ChangePasswordByOTPViewModel model)
        {
            var peticion = await _repository.ChangePasswordByOTP(model);
            var response = peticion[0];

            if (response.Codigo != "-1")
            {
                var user = new
                {
                    Name = response.StrName,
                    Token = _Token.CreateToken(new[] { new Claim("User_Id", response.Id.ToString()) }, TimeSpan.FromMinutes(30))
                };
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Codigo = response.Codigo,
                    User = user
                });
            }
            else
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Codigo = response.Codigo
                });
            }
        }
    }
}