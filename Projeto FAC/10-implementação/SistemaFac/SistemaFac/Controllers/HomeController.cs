using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaFac.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "APLICATIVO WEB DE CATALOGO DE SERVIÇOS.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "esodegemeos@gmail.com";

            return View();
        }
    }
}