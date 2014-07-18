using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocsDigitalesApp.Models
{
    public class UsuarioModel
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Debe capturar su nombre de Usuario")]
        public string Nombre { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Por favor introduzca una dirección de correo electrónico válida")]
        [Required(ErrorMessage = "Debe capturar su Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Debe capturar su RFC")]
        public string RFC { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de su empresa")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "Debe capturar su Contraseña")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Debe capturar su Contraseña")]
        [DataType(DataType.Password)]
        public string ContrasenaConfirm { get; set; }

        //private int _nIdUsuario;
        //private string _strNombre;
        //private string _strCorreoElectornico;
        //private string _strRFC;
        //private string _strContrasena;
        //private int _nIdEmpresa;

        //public int IdUsuario
        //{
        //    get { return _nIdUsuario; }
        //    set { _nIdUsuario = value; }
        //}
        //public string Nombre
        //{
        //    get { return _strNombre; }
        //    set { _strNombre = value; }
        //}


        //public string CorreoElectronico
        //{
        //    get { return _strCorreoElectornico; }
        //    set { _strCorreoElectornico = value; }
        //}
        //public string RFC
        //{
        //    get { return _strRFC; }
        //    set { _strRFC = value; }
        //}
        //public string Contrasena
        //{
        //    get { return _strContrasena; }
        //    set { _strContrasena = value; }
        //}
        //public int IdEmpresa
        //{
        //    get { return _nIdEmpresa; }
        //    set { _nIdEmpresa = value; }
        //}
    }
}