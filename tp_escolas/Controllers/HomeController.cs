using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tp_escolas.Models;
namespace tp_escolas.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Random rnd = new Random();
            var db = new EscolaContext();
            var lstAv = db.Avaliacoes.ToList();
            List<Avaliacao> ListaFinal = new List<Avaliacao>();

            

            if (lstAv.Count > 0 && lstAv.Count > 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    var index = rnd.Next(lstAv.Count);
                    ListaFinal.Add(lstAv[index]);
                }

             
                return View(ListaFinal);
            }
            else
                return View(lstAv);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}