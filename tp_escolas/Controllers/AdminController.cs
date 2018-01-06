using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tp_escolas.Models;

namespace tp_escolas.Controllers
{
    [Authorize(Roles = RolesConst.Admin)]
    public class AdminController : Controller
    {
        private EscolaContext _db;
        private ApplicationDbContext _UserDb;

        public AdminController()
        {
            _db = new EscolaContext();
            _UserDb = new ApplicationDbContext();

        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Aceitar()
        {
            return View(_db.Instituicoes.Where(w => w.Activa == false).ToList());
        }
        public ActionResult AceitarInst(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("Index");
            try
            {
                var inst = _db.Instituicoes.Find(id);
                inst.Activa = true;
                _db.SaveChanges();

            }
            catch
            {

            }
            return RedirectToAction("Aceitar");
        }

        public ActionResult AddServ()
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult AddServ(Servico serv)
        {
            try
            {
                // TODO: Add delete logic here
                if (serv.Descricao.TrimStart() == "" || serv.Descricao == null)
                    ModelState.AddModelError("Descricao", "Por favor insira uma descrição");
                if (ModelState.IsValid)
                {
                    _db.Servicos.Add(serv);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }


        public ActionResult AddTipoEnsino()
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult AddTipoEnsino(TipoEnsino te)
        {
            try
            {
                // TODO: Add delete logic here
                if (te.Descricao.TrimStart() == "" || te.Descricao == null)
                    ModelState.AddModelError("Descricao", "Por favor insira uma descrição");
                if (ModelState.IsValid)
                {
                    _db.TipoEnsino.Add(te);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ServicoDelEdit()
        {
            return View(_db.Servicos.ToList());
        }
        public ActionResult EditServ(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("ServicoDelEdit");
            return View(_db.Servicos.Find(id));
        }
        [HttpPost]
        public ActionResult EditServ(Servico serv)
        {
            try
            {

                if (serv.Descricao.TrimStart() == "" || serv.Descricao == null)
                    ModelState.AddModelError("Descricao", "Por favor insira uma descrição");
                if (ModelState.IsValid)
                {
                    var servico = _db.Servicos.Find(serv.ServicosID);
                    servico.Descricao = serv.Descricao;
                    _db.SaveChanges();
                    return RedirectToAction("ServicoDelEdit");
                }
            }
            catch
            {

            }
            return RedirectToAction("ServicoDelEdit");
        }
        public ActionResult DelServ(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                    return RedirectToAction("ServicoDelEdit");
                _db.Servicos.Remove(_db.Servicos.Find(id));
                _db.SaveChanges();
            }
            catch  
            {
                
            }
            return RedirectToAction("ServicoDelEdit");
        }

        public ActionResult TipoEnsinoDelEdit()
        {
            return View(_db.TipoEnsino.ToList());
        }
        public ActionResult EditTEnsino(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("TipoEnsinoDelEdit");
            return View(_db.TipoEnsino.Find(id));
        }
        [HttpPost]
        public ActionResult EditTEnsino(TipoEnsino te)
        {
            try
            {

                if (te.Descricao.TrimStart() == "" || te.Descricao == null)
                    ModelState.AddModelError("Descricao", "Por favor insira uma descrição");
                if (ModelState.IsValid)
                {
                    var Tens = _db.TipoEnsino.Find(te.TipoEnsinoID);
                    Tens.Descricao = te.Descricao;
                    _db.SaveChanges();
                    return RedirectToAction("TipoEnsinoDelEdit");
                }
            }
            catch
            {

            }
            return RedirectToAction("TipoEnsinoDelEdit");
        }
        public ActionResult DelTEnsino(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                    return RedirectToAction("TipoEnsinoDelEdit");
                _db.TipoEnsino.Remove(_db.TipoEnsino.Find(id));
                _db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("TipoEnsinoDelEdit");
        }
    }
}
