using Microsoft.AspNetCore.Mvc;
using TallerMVC.Data;
using TallerMVC.Models;

namespace TallerMVC.Controllers
{
    public class CitasController : Controller
    {
        CitasDatos _citasDatos = new CitasDatos();
        public IActionResult Index(int id)
        {
            id = int.Parse(HttpContext.Request.Cookies["UserId"]);
            var oLista = _citasDatos.Listar(id);
            return View(oLista);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CitasDetalles citasDetalles)
        {
            citasDetalles.usuario_id = int.Parse(HttpContext.Request.Cookies["UserId"]);
            citasDetalles.vehiculo_id = int.Parse(HttpContext.Request.Cookies["VehiculoId"]);



            var respuesta = _citasDatos.Guardar(citasDetalles);

            if (respuesta)
            {
                return RedirectToAction("index", "Citas");
            }

            ViewBag.Error = "No se pudo registrar el auto.";
            return View();
        }
    }
}
