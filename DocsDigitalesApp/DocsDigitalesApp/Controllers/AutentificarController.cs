using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocsDigitalesApp.Models;

namespace DocsDigitalesApp.Controllers
{
    public class AutentificarController : Controller
    {
        //
        // GET: /Autentificar/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AutentificarModel model)
        {

            if (ModelState.IsValid)
            {
                var usuarios = model;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //
        // GET: /Autentificar/Create

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
