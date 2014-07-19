using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocsDigitalesApp.Models
{
    public class EmpresaModel
    {
        [Key]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Debe capturar el nombre de la empresa")]
        public string Nombre { get; set; }
    }
}