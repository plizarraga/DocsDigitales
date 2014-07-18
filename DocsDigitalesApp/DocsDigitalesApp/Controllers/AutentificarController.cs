using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocsDigitalesApp.Models;
using System.Web.Security;

namespace DocsDigitalesApp.Controllers
{
    public class AutentificarController : Controller
    {
        // Manda llamar la vista de Login para iniciar sesion
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AutentificarModel model)
        {

            if (ModelState.IsValid)
            {
                var usuarios = model;
                FormsAuthentication.SetAuthCookie(model.correoElectronico, false);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Cierra la sesion
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Autentificar");
        }

        // Llama a la vista para crear un nuevo usuario
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Autentificar/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }      
    }
}
