using Microsoft.AspNetCore.Mvc;
using TallerMVC.Data;
using TallerMVC.Models;

namespace TallerMVC.Controllers
{
    public class CitasController : Controller
    {
        CitasDatos _citasDatos = new CitasDatos();
        ValidarData data = new ValidarData();
        public IActionResult Index(int id)
        {
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
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
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
            var Cita = _citasDatos.obtenerCita(id);
            return View(Cita);
        }
        public IActionResult Eliminar(int id)
        {
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
            var Cita = _citasDatos.obtenerCita(id);
            return View(Cita);
        }
        [HttpPost]
        public IActionResult Register(CitasDetalles citasDetalles)
        {
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
            citasDetalles.usuario_id = int.Parse(HttpContext.Request.Cookies["UserId"]);
            citasDetalles.vehiculo_id = int.Parse(HttpContext.Request.Cookies["VehiculoId"]);

            DateTime now = DateTime.Now;

            // Verificar que la hora ingresada no sea anterior a la hora actual
            if (citasDetalles.fecha < now)
            {
                ViewBag.Error = "La fecha ingresada no puede ser anterior a la fecha acutal";
                return View();
            }

            //validar si la fecha y hora no estan ocupadas
            var validacion = data.ValidarCita(citasDetalles.hora, citasDetalles.fecha);

            if (validacion>0)
            {
                ViewBag.Error = "La fecha ingresada ya esta ocupada";
                return View();
            }

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
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
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
            if (HttpContext.Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("ErrorCustom", "Home");
            }
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
