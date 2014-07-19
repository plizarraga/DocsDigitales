using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace DocsDigitalesApp.Models
{
    public class SucursalesRepo
    {
        private static string _conString = ConfigurationManager.ConnectionStrings["conMySQL"].ConnectionString.ToString();

        // Devuelve el listado de sucursales
        public static IEnumerable<SucursalViewModel> GetSucursales(int id_empresa)
        {
            using (MySqlConnection cn = new MySqlConnection(_conString))
            {
                cn.Open();

                var cmd = new MySqlCommand("SP_SEL_SUCURSALES_POR_ID_EMPRESA", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("pi_id_empresa", id_empresa);

                var da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cn.Close();

                List<SucursalViewModel> listSucursales = new List<SucursalViewModel>();
                SucursalViewModel item;

                foreach (DataRow dr in dt.Rows)
                {
                    item = new SucursalViewModel();

                    item.id_empresa = Convert.ToInt32(dr["id_empresa"]);
                    item.id_sucursal = Convert.ToInt32(dr["id_sucursal"].ToString());
                    item.nombre = dr["nombre"].ToString();
                    item.calle = dr["calle"].ToString();
                    item.colonia = dr["colonia"].ToString();
                    item.num_ext = Convert.ToInt32(dr["num_ext"]);
                    item.num_int = Convert.ToInt32(dr["num_int"]);
                    item.codigo_postal = Convert.ToInt32(dr["codigo_postal"]);
                    item.ciudad = dr["ciudad"].ToString();
                    item.pais = dr["pais"].ToString();
                    item.no_empleados = Convert.ToInt32(dr["no_empleados"]);
                    listSucursales.Add(item);
                }

                return listSucursales;
            }
        }
    }
}