using Dapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using SM_ProyectoAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SM_ProyectoAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
        public HomeController(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        [HttpPost]
        [Route("Registro")]
        public IActionResult Registro(RegistroUsuarioRequestModel usuario)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Identificacion", usuario.Identificacion);
                parametros.Add("@Nombre", usuario.Nombre);
                parametros.Add("@CorreoElectronico", usuario.CorreoElectronico);
                parametros.Add("@Contrasenna", usuario.Contrasenna);
                var resultado = context.Execute("Registro", parametros);
                return Ok(resultado);
            }
        }


        [HttpPost]
        [Route("IniciarSesion")]
        public IActionResult IniciarSesion(ValidarSesionRequestModel usuario)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@CorreoElectronico", usuario.CorreoElectronico);
                parametros.Add("@Contrasenna", usuario.Contrasenna);
                var resultado = context.QueryFirstOrDefault<ValidarSesionResponse>("ValidarInicioSesion", parametros);

                if (resultado != null)
                {
                    resultado.Token = GenerarToken(resultado.ConsecutivoUsuario, resultado.Nombre, resultado.ConsecutivoPerfil);
                    return Ok(resultado);
                }

                return NotFound();
            }
        }


        [HttpGet]
        [Route("RecuperarAcceso")]
        public async Task<IActionResult> RecuperarAcceso(string CorreoElectronico)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@CorreoElectronico", CorreoElectronico);
                var resultado = context.QueryFirstOrDefault<ValidarSesionResponse>("ValidarUsuario", parametros);

                if (resultado != null)
                {
                    //Actualizar al contraseña
                    var contrasennaGenerada = GenerarContrasena();

                    var parametrosActualizar = new DynamicParameters();
                    parametrosActualizar.Add("@ConsecutivoUsuario", resultado.ConsecutivoUsuario);
                    parametrosActualizar.Add("@Contrasenna", contrasennaGenerada);
                    var resultadoActualizar = context.Execute("ActualizarContrasenna", parametrosActualizar);

                    if (resultadoActualizar > 0)
                    {
                        //Enviar correo
                        var ruta = Path.Combine(_environment.ContentRootPath, "PlantillaCorreo.html");
                        var html = System.IO.File.ReadAllText(ruta, Encoding.UTF8);

                        html = html.Replace("{{Nombre}}", resultado.Nombre);
                        html = html.Replace("{{Contrasenna}}", contrasennaGenerada);

                        await EnviarCorreo(resultado.CorreoElectronico, "Recuperar Acceso", html);
                        return Ok(resultado);
                    }
                }

                return NotFound();
            }
        }


        private string GenerarContrasena()
        {
            int longitud = 8;
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder contrasena = new StringBuilder();
            byte[] datosAleatorios = new byte[longitud];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(datosAleatorios);
            }

            for (int i = 0; i < longitud; i++)
            {
                int indice = datosAleatorios[i] % caracteres.Length;
                contrasena.Append(caracteres[indice]);
            }

            return contrasena.ToString();
        }

        private async Task EnviarCorreo(string destinatario, string asunto, string cuerpoHtml)
        {
            var correoSMTP = _configuration["Valores:CorreoSMTP"];
            var contrasennaSMTP = _configuration["Valores:ContrasennaSMTP"];

            if (string.IsNullOrEmpty(contrasennaSMTP))
                return;

            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("SM Web", correoSMTP));
            mensaje.To.Add(MailboxAddress.Parse(destinatario));
            mensaje.Subject = asunto;
            mensaje.Body = new TextPart("html") { Text = cuerpoHtml };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(correoSMTP, contrasennaSMTP);

                await smtp.SendAsync(mensaje);
                await smtp.DisconnectAsync(true);
            }
        }

        private string GenerarToken(int userId, string userName, int userRol)
        {
            var key = _configuration["Valores:KeyJWT"]!;

            var claims = new List<Claim>
            {
                new Claim("userId", userId.ToString()),
                new Claim("userName", userName),
                new Claim("userRol", userRol.ToString())
            };

            // Crear la clave y credenciales de firma
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
