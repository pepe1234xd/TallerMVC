using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using TallerMVC.Data;
using TallerMVC.Models;
using TallerMVC.Models.DTO;

namespace TallerMVC.Controllers
{
    public class UsuariosController : Controller
    {
        UsuarioDatos _usuariodatos = new UsuarioDatos();
        private readonly AesEncryption _aesEncryption;

        public UsuariosController(AesEncryption aesEncryption)
        {
            _aesEncryption = aesEncryption;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UsuariosRegisterDTO oUsuario)
        {
            //validacion de contrasenias iguales
            if (oUsuario.contrasenia != oUsuario.confirmarContrasenia)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }

            //encriptacion de contrasenia
            oUsuario.contrasenia = _aesEncryption.Encrypt(oUsuario.contrasenia);

            // Verificar si el correo electrónico ya está en uso
            Usuarios usuarioExistente = _usuariodatos.obtenerUsuario(oUsuario.email);
            if (usuarioExistente != null)
            {
                ViewBag.Error = "El correo electrónico ya está registrado.";
                return View();
            }

            //transformar de dto a un objeto de usuario nuevo
            var Usuario = new Usuarios
            {
                nombres = oUsuario.nombres,
                apellidos= oUsuario.apellidos,
                email= oUsuario.email,
                contrasenia = oUsuario.contrasenia

            };  

            // Registrar al nuevo usuario
            var respuesta = _usuariodatos.Guardar(Usuario);

            if (respuesta)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = "No se pudo registrar al usuario.";
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string contrasenia)
        {
            contrasenia = _aesEncryption.Encrypt(contrasenia);
            string contraseniaCorta = contrasenia.Substring(0, Math.Min(20, contrasenia.Length));
            Usuarios usuario = _usuariodatos.obtenerUsuario(email);

            //validar si el correo existe
            if (usuario.email != null)
            {
                //validar si la contrasenia es la misma
                if (usuario.contrasenia == contraseniaCorta)
                {
                    Response.Cookies.Append("UserId", usuario.id.ToString());
                    return RedirectToAction("Index", "Citas", usuario.id);
                }
            }
            // Si el usuario no está registrado o las contraseñas no coinciden, mostrar un mensaje de error.
            ViewBag.Error = "Correo electrónico o contraseña inválidos.";
            return View();
        }
    }
}
