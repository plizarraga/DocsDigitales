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
            List<SelectListItem> sucursal = new List<SelectListItem>();

            sucursal.Add(new SelectListItem { Text = "-- SELECCIONAR SUCURSAL --", Value = "-1" });
            sucursal.Add(new SelectListItem { Text = "uno", Value = "unos" });
            sucursal.Add(new SelectListItem { Text = "dos", Value = "dos" });
            sucursal.Add(new SelectListItem { Text = "tres", Value = "tres" });

            ViewBag.Sucursal = sucursal;
            UsuarioViwModel usuario = UsuarioRepo.GetUsuario(User.Identity.Name);
            return View(EmpleadoRepo.GetEmpleados(usuario.Id_Empresa));
        }

        //
        // GET: /Empleado/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Empleado/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Empleado/Create

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

        //
        // GET: /Empleado/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Empleado/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Empleado/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Empleado/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
