using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocsDigitalesApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // Manda llamar a la vista de Pagina de Inicio
        public ActionResult Index()
        {
            return View();
        }

    }
}
