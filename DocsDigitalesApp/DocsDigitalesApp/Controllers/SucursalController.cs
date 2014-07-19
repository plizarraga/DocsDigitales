﻿using DocsDigitalesApp.Models;
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

        public ActionResult Sucursales()
        {
            UsuarioViwModel usuario = UsuarioRepo.GetUsuario(User.Identity.Name);
            return View(SucursalesRepo.GetSucursales(usuario.Id_Empresa));
        }
    }
}
