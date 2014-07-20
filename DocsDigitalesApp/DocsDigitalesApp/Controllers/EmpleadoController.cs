using DocsDigitalesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocsDigitalesApp.Controllers
{
    public class EmpleadoController : Controller
    {
        // Vista con el listado de empelados
        // GET: /Empleado/

        public ActionResult Index()
        {
            UsuarioViwModel usuario = UsuarioRepo.GetUsuario(User.Identity.Name);
            ViewBag.Sucursal = SucursalesRepo.FillSucursales(usuario.Id_Empresa); ;

            return View(EmpleadoRepo.GetEmpleados(usuario.Id_Empresa));
        }

        
    }
}
