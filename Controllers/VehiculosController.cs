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

        [HttpPost]
        public IActionResult Register(vehiculos ovehiculos)
        {
            ovehiculos.usuario_id = int.Parse(HttpContext.Request.Cookies["UserId"]);
            var respuesta = vehiculosDatos.Guardar(ovehiculos);

            if (respuesta)
            {
                return RedirectToAction("index");
            }

            ViewBag.Error = "No se pudo registrar el auto.";
            return View();
        }

        public IActionResult Seleccionar(int id)
        {
            Response.Cookies.Append("VehiculoId", id.ToString());
            return RedirectToAction("Register","Citas");
        }
    }
}
