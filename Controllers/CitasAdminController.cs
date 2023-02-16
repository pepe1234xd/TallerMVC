using Microsoft.AspNetCore.Mvc;
using TallerMVC.Data;

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
    }
}
