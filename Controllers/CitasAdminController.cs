using Microsoft.AspNetCore.Mvc;
using TallerMVC.Data;
using TallerMVC.Models;
using TallerMVC.Models.DTO;

namespace TallerMVC.Controllers
{
    public class CitasAdminController : Controller
    {
        CitasAdmin _citasAdmin = new CitasAdmin();
        public IActionResult Index()
        {
            var oLista = _citasAdmin.Listar();
            return View(oLista);
        }
        public IActionResult Editar(int id)
        {
            var Cita = _citasAdmin.obtenerCita(id);
            return View(Cita);
        }
        public IActionResult Eliminar(int id)
        {
            var Cita = _citasAdmin.obtenerCita(id);
            return View(Cita);
        }
        [HttpPost]
        public IActionResult Editar(TallerMVC.Models.DTO.CitasView citasView)
        {
            var Cita = _citasAdmin.Editar(citasView);
            if (Cita)
            {
                return RedirectToAction("Index", "CitasAdmin");
            }

            ViewBag.Error = "No se pudo editar la cita.";
            Thread.Sleep(3000); // Wait for 3 seconds
            return RedirectToAction("Index", "CitasAdmin");
        }
        [HttpPost]
        public IActionResult Eliminar(CitasView citasView)
        {
            var respuesta = _citasAdmin.Eliminar(citasView.id);
            if (respuesta)
            {
                return RedirectToAction("index", "CitasAdmin");
            }

            ViewBag.Error = "No se pudo eliminar la cita";
            Thread.Sleep(3000); // Wait for 3 seconds
            return RedirectToAction("Index", "CitasAdmin");
        }

    }
}
