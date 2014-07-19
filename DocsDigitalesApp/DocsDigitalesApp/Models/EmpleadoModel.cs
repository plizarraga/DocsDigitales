using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocsDigitalesApp.Models
{
    public class EmpleadoModel
    {
        [Key]
        public int id_empleado { get; set; }

        [Required(ErrorMessage = "Debe capturar el Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe capturar el RFC")]
        public string rfc { get; set; }

        [Required(ErrorMessage = "Debe capturar el Puesto")]
        public string puesto { get; set; }

        [Required(ErrorMessage = "Debe capturar la sucursal")]
        public int id_sucursal { get; set; }

    }

    public class EmpleadoViewModel : EmpleadoModel
    {
        public string sucursal { get; set; }
    }
}