using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocsDigitalesApp.Models;
using System.Web.Security;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DocsDigitalesApp.Controllers
{
    public class AutentificarController : Controller
    {
        private string _conString = ConfigurationManager.ConnectionStrings["conMySQL"].ConnectionString.ToString();

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
        public JsonResult Create(UsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                using (MySqlConnection cn = new MySqlConnection(_conString))
                {
                    var cmd = new MySqlCommand("SP_INS_EMPRESA_USUARIO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("pi_nombre_usuario", model.Nombre.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("pi_correo_electronico", model.CorreoElectronico.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("pi_rfc", model.RFC.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("pi_contrasena", model.Contrasena.Trim());
                    cmd.Parameters.AddWithValue("pi_nombre_empresa", model.Empresa.Trim().ToUpper());

                    cn.Open();

                    cmd.ExecuteScalar();
                    cn.Close();
                    return Json(new { respuesta = "Se registro el usuario y la empresa", error = "" });
                }               
            }
            else
            {
                return Json(new { respuesta = "", error = "Modelo no valido" });
            }

        }

        // Verificar si existe la empresa
        [HttpPost]
        public int IfExistEmpresa(string Nombre)
        {
            using (MySqlConnection cn = new MySqlConnection(_conString))
            {
                var cmd = new MySqlCommand("SP_SEL_EMPRESA_POR_NOMBRE", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("pi_nombre", Nombre);
                cn.Open();

                int Found = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();

                return Found;
            }
        }
    }
}
