using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocsDigitalesApp.Models
{
    public class AutentificarModel
    {

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Por favor introduzca una dirección de correo electrónico válida")]
        [Required(ErrorMessage = "Debe capturar su Correo Electrónico")]

        [DataType(DataType.EmailAddress)]
        public string correoElectronico { get; set; }

        [Required(ErrorMessage = "Debe capturar su Contraseña")]
        [DataType(DataType.Password)]
        public string contrasena { get; set; }
    }
}