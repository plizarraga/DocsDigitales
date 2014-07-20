using DocsDigitalesApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocsDigitalesApp.Controllers
{
    public class EmpleadoController : Controller
    {
        private string _conString = ConfigurationManager.ConnectionStrings["conMySQL"].ConnectionString.ToString();

        // Vista con el listado de empelados
        // GET: /Empleado/

        public ActionResult Index()
        {
            UsuarioViwModel usuario = UsuarioRepo.GetUsuario(User.Identity.Name);
            ViewBag.frmSucursal = SucursalesRepo.FillSucursales(usuario.Id_Empresa); ;

            return View();
        }

        // Insertar el empleado
        [HttpPost]
        public JsonResult AddEmpleado(EmpleadoModel model)
        {
            try
            {
                using (MySqlConnection cn = new MySqlConnection(_conString))
                {
                    UsuarioViwModel usuario = UsuarioRepo.GetUsuario(User.Identity.Name);

                    var cmd = new MySqlCommand("SP_INS_EMPLEADO", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("pi_nombre", model.Nombre.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("pi_rfc", model.rfc.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("pi_puesto", String.IsNullOrEmpty(model.puesto) ? "" : model.puesto.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("pi_id_sucursal", model.id_sucursal);

                    cn.Open();

                    cmd.ExecuteScalar();
                    cn.Close();
                    return Json(new { respuesta = "Empelado registrado exitosamente", error = "" });
                }
            }
            catch (Exception e)
            {
                return Json(new { respuesta = "", error = e.Message.ToString() });
            }
        }

        [HttpPost]
        public JsonResult GetEmpleados()
        {
            UsuarioViwModel usuario = UsuarioRepo.GetUsuario(User.Identity.Name);
            return Json(new { Listado = EmpleadoRepo.GetEmpleados(usuario.Id_Empresa) });
        } 
    }
}
