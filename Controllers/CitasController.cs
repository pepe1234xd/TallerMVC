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
        public IActionResult Editar(int id) 
        {           
            var Cita = _citasDatos.obtenerCita(id);
            return View(Cita);
        }
        public IActionResult Eliminar(int id)
        {
            var Cita = _citasDatos.obtenerCita(id);
            return View(Cita);
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
        [HttpPost]
        public IActionResult Editar(CitasDetalles citasDetalles)
        {
            var Cita = _citasDatos.Editar(citasDetalles);

            if (Cita)
            {
                return RedirectToAction("Index", "Citas");
            }

            ViewBag.Error = "No se pudo editar la cita.";
            return View();
        }
        [HttpPost]
        public IActionResult Eliminar(CitasDetalles citasDetalles)
        {
            var respuesta = _citasDatos.Eliminar(citasDetalles.id);
            if (respuesta)
            {
                return RedirectToAction("index", "Citas");
            }
            ViewBag.Error = "No se pudo eliminar el auto";
            return View();
        }
    }
}
