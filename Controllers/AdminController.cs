using Microsoft.AspNetCore.Mvc;
using TallerMVC.Data;
using TallerMVC.Models.DTO;
using TallerMVC.Models;

namespace TallerMVC.Controllers
{
    public class AdminController : Controller
    {
        AdminDatos _admindatos = new AdminDatos();
        private readonly AesEncryption _aesEncryption;

        public AdminController(AesEncryption aesEncryption)
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
        public IActionResult Register(AdminRegisterDTO oAdmin)
        {
            //validacion de contrasenias iguales
            if (oAdmin.contrasenia != oAdmin.confirmarContrasenia)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }

            //encriptacion de contrasenia
            oAdmin.contrasenia = _aesEncryption.Encrypt(oAdmin.contrasenia);

            // Verificar si el correo electrónico ya está en uso
            Admins adminExistente = _admindatos.obtenerAdmin(oAdmin.email);
            if (adminExistente != null)
            {
                ViewBag.Error = "El correo electrónico ya está registrado.";
                return View();
            }

            //transformar de dto a un objeto de admin nuevo
            var Admin = new Admins
            {
                nombres = oAdmin.nombres,
                apellidos = oAdmin.apellidos,
                email = oAdmin.email,
                contrasenia = oAdmin.contrasenia,
                puesto = oAdmin.puesto,
            };

            // Registrar al nuevo admin
            var respuesta = _admindatos.Guardar(Admin);

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
            Admins admin = _admindatos.obtenerAdmin(email);

            //validar si el correo existe
            if (admin.email != null)
            {
                //validar si la contrasenia es la misma
                if (admin.contrasenia == contraseniaCorta)
                {
                    Response.Cookies.Append("AdminId", admin.id.ToString());
                    return RedirectToAction("Index", "CitasAdmin");
                }
            }
            // Si el usuario no está registrado o las contraseñas no coinciden, mostrar un mensaje de error.
            ViewBag.Error = "Correo electrónico o contraseña inválidos.";
            return View();
        }
    }
}
