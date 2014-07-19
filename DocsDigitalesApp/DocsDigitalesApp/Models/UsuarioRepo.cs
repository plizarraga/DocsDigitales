using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace DocsDigitalesApp.Models
{
    public class UsuarioRepo
    {
        private static string _conString = ConfigurationManager.ConnectionStrings["conMySQL"].ConnectionString.ToString();

        // Devuelve los datos de un usuario
        public static UsuarioViwModel GetUsuario(string correo_electronico)
        {
            using (MySqlConnection cn = new MySqlConnection(_conString))
            {
                cn.Open();

                var cmd = new MySqlCommand("SP_SEL_USUARIO_POR_CORREO", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("pi_correo_electronico", correo_electronico);

                var da = new MySqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                cn.Close();

                UsuarioViwModel usuario = new UsuarioViwModel();

                foreach (DataRow dr in dt.Rows)
                {
                    usuario.IdUsuario = Convert.ToInt32(dr["id_usuario"].ToString());
                    usuario.Nombre = dr["nombre"].ToString();
                    usuario.Id_Empresa = Convert.ToInt32(dr["id_empresa"]);           
                    usuario.CorreoElectronico = dr["correo_electronico"].ToString();
                    usuario.RFC = dr["rfc"].ToString();
                    usuario.Empresa = dr["empresa"].ToString();
                }

                return usuario;
            }
        }
    }
}