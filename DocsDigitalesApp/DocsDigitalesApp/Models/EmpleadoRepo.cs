using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace DocsDigitalesApp.Models
{
    public class EmpleadoRepo
    {
        private static string _conString = ConfigurationManager.ConnectionStrings["conMySQL"].ConnectionString.ToString();

        // Devuelve el listado de sucursales
        public static IEnumerable<EmpleadoViewModel> GetEmpleados(int id_empresa)
        {
            using (MySqlConnection cn = new MySqlConnection(_conString))
            {
                cn.Open();

                var cmd = new MySqlCommand("SP_SEL_EMPLEADOS_POR_EMPRESA", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("pi_id_empresa", id_empresa);

                var da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cn.Close();

                List<EmpleadoViewModel> listEmpleados = new List<EmpleadoViewModel>();
                EmpleadoViewModel item;

                foreach (DataRow dr in dt.Rows)
                {
                    item = new EmpleadoViewModel();

                    item.id_empleado = Convert.ToInt32(dr["id_empelado"]);
                    item.Nombre = dr["nombre"].ToString();
                    item.rfc = dr["rfc"].ToString();
                    item.puesto = dr["puesto"].ToString();
                    item.id_sucursal = Convert.ToInt32(dr["id_sucursal"]);
                    item.sucursal = dr["sucursal"].ToString();
                    listEmpleados.Add(item);
                }

                return listEmpleados;
            }
        }
    }
}