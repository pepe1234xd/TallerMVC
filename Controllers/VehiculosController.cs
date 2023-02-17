using Microsoft.AspNetCore.Mvc;
using TallerMVC.Data;
using TallerMVC.Models.DTO;
using TallerMVC.Models;

namespace TallerMVC.Controllers
{
    
    public class VehiculosController : Controller
    {        
        VehiculosDatos vehiculosDatos = new VehiculosDatos();
        public IActionResult Index(int id)
        {
            id = int.Parse(HttpContext.Request.Cookies["UserId"]);
            var oLista = vehiculosDatos.Listar(id);
            return View(oLista);
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
            var vehiculo = vehiculosDatos.obtenerVehiculo(id);
            return View(vehiculo);
        }
        public IActionResult Eliminar(int id)
        {
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
            var vehiculo = vehiculosDatos.obtenerVehiculo(id);
            return View(vehiculo);
        }
        public IActionResult Seleccionar(int id)
        {
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
            Response.Cookies.Append("VehiculoId", id.ToString());
            return RedirectToAction("Register", "Citas");
        }

        [HttpPost]
        public IActionResult Register(vehiculos ovehiculos)
        {
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
            ovehiculos.usuario_id = int.Parse(HttpContext.Request.Cookies["UserId"]);
            var respuesta = vehiculosDatos.Guardar(ovehiculos);

            if (respuesta)
            {
                return RedirectToAction("index");
            }

            ViewBag.Error = "No se pudo registrar el auto.";
            return View();
        }
        [HttpPost]
        public IActionResult Editar(vehiculos ovehiculos)
        {
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
            var respuesta = vehiculosDatos.Editar(ovehiculos);

            if (respuesta)
            {
                return RedirectToAction("index");
            }

            ViewBag.Error = "No se pudo registrar el auto.";
            return View();
        }
        [HttpPost]
        public IActionResult Eliminar(vehiculos ovehiculos)
        {
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
            var respuesta = vehiculosDatos.Eliminar(ovehiculos.id);
            if (respuesta)
            {
                return RedirectToAction("index", "vehiculos");
            }

            ViewBag.Error = "No se pudo registrar el auto.";
            return View();
        }

    }
}
