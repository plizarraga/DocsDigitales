using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocsDigitalesApp.Models
{
    public class SucursalModel
    {
        public int id_sucursal { get; set; }
        public int id_empresa { get; set; }
        public string nombre { get; set; } 
        public string calle { get; set; } 
        public string colonia { get; set; } 
        public int num_ext { get; set; }
        public int num_int { get; set; }
        public int codigo_postal { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
    }
}