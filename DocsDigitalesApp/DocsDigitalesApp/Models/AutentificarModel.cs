using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocsDigitalesApp.Models
{
    public class AutentificarModel
    {
        [Required(ErrorMessage = "Debe capturar su Correo Electronico")]
        public string correoElectronico { get; set; }

        [Required(ErrorMessage = "Debe capturar su Contraseña")]
        public string contrasena { get; set; }
    }
}