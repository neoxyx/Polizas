using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using ProyectoPolizas.Models;

namespace ProyectoPolizas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public AuthController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            // Validar las credenciales del usuario aquí
            if (IsValidUser(credentials))
            {
                // Autenticación exitosa
                // Genera y devuelve el token JWT
                var token = GenerateJwtToken(credentials.Username);
                return Ok(new { token });
            }

            // Autenticación fallida
            return Unauthorized();
        }


        // Simulación de verificación de credenciales (reemplazar con tu propia lógica).
        private bool IsValidUser(UserCredentials credentials)
        {
            // Compara las credenciales con valores predefinidos (solo para demostración)
            string validUsername = "usuarioDemo";
            string validPassword = "passwordDemo";

            // Verifica si las credenciales coinciden con los valores predefinidos
            return credentials.Username == validUsername && credentials.Password == validPassword;
        }

        // Generar un token JWT
        private string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, username)
            }),
                Expires = DateTime.UtcNow.AddHours(1), // Tiempo de expiración del token
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }


    public class AppSettings
    {
        public string Secret { get; set; }
    }
}
