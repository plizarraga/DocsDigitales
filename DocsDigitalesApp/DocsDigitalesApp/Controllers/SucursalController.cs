using DocsDigitalesApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocsDigitalesApp.Controllers
{
    [Authorize]
    public class SucursalController : Controller
    {
        private string _conString = ConfigurationManager.ConnectionStrings["conMySQL"].ConnectionString.ToString();

        // Listado de sucursales
        public ActionResult Index()
        {
            UsuarioViwModel usuario = UsuarioRepo.GetUsuario(User.Identity.Name);
            return View(SucursalesRepo.GetSucursales(usuario.Id_Empresa));
        }

        // Insertar la sucursal en la base de datos
        [HttpPost]
        public JsonResult AddSucursal(SucursalModel model)
        {
            try
            {
                using (MySqlConnection cn = new MySqlConnection(_conString))
                {
                    UsuarioViwModel usuario = UsuarioRepo.GetUsuario(User.Identity.Name);

                    var cmd = new MySqlCommand("SP_INS_SUCURSAL", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("pi_id_empresa", usuario.Id_Empresa);
                    cmd.Parameters.AddWithValue("pi_nombre", model.nombre.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("pi_calle", String.IsNullOrEmpty(model.calle) ? "" : model.calle.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("pi_colonia", String.IsNullOrEmpty(model.colonia) ? "" : model.colonia.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("pi_num_ext", Convert.ToInt32(model.num_ext));
                    cmd.Parameters.AddWithValue("pi_num_int", Convert.ToInt32(model.num_int));
                    cmd.Parameters.AddWithValue("pi_cp", Convert.ToInt32(model.codigo_postal));
                    cmd.Parameters.AddWithValue("pi_ciudad", String.IsNullOrEmpty(model.ciudad) ? "" : model.ciudad.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("pi_pais", String.IsNullOrEmpty(model.pais) ? "" : model.pais.Trim().ToUpper());

                    cn.Open();

                    cmd.ExecuteScalar();
                    cn.Close();
                    return Json(new { respuesta = "Sucursal registrada exitosamente", error = "" });
                }
            }
            catch (Exception e)
            {
                return Json(new { respuesta = "", error = e.Message.ToString() });
            }
        }

        // Verificar si existe la sucursal en la base de datos
        [HttpPost]
        public JsonResult IfExistSucursal(string NombreSucursal)
        {
            using (MySqlConnection cn = new MySqlConnection(_conString))
            {
                UsuarioViwModel usuario = UsuarioRepo.GetUsuario(User.Identity.Name);
                
                // Verificar si existe la empresa
                var cmd = new MySqlCommand("SP_SEL_SUCURSAL_POR_NOMBRE", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("pi_nombre", NombreSucursal);
                cmd.Parameters.AddWithValue("pi_id_empresa", usuario.Id_Empresa);
                cn.Open();

                int FoundSucursal = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();

                return Json(new { sucursal = FoundSucursal });
            }
        }
    }
}
