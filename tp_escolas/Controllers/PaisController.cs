using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using tp_escolas.Models;
using tp_escolas.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace tp_escolas.Controllers
{
    [Authorize(Roles = RolesConst.Pai)]
    public class PaisController : Controller
    {

        private EscolaContext _db;
        private ApplicationDbContext _UserDb;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public PaisController()
        {
            _db = new EscolaContext();
            _UserDb = new ApplicationDbContext();

        }
        public PaisController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Pais
        public ActionResult Index()
        {
            var uId = User.Identity.GetUserId();
            ViewBag.Id = _db.Pais.Where(u => u.UserID == uId).Select(u => u.PaisID).FirstOrDefault();
            return View();
        }

        //Get: Pais/Registo
        [AllowAnonymous]
        public ActionResult Registo()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            ViewBag.CidadeID = new SelectList(_db.Cidades, "CidadeID", "CidadeNome");
            return View();
        }

        // GET: Pais/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // POST: Pais/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Registo(PaiViewModelAdd pai)
        {
            try
            {

                ViewBag.CidadeID = new SelectList(_db.Cidades, "CidadeID", "CidadeNome");


                if (_UserDb.Users.Any(y => y.Email == pai.Email))
                {
                    ModelState.AddModelError("Email", "Conta já existente!");

                    return View();
                }
                if (pai.Cidade.CidadeID.ToString() == "")
                {
                    return View();
                }

                if (ModelState.IsValid)
                {

                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_UserDb));
                    var user = new ApplicationUser { UserName = pai.Email, Email = pai.Email };
                    var result = await UserManager.CreateAsync(user, pai.Password);

                    if (result.Succeeded)
                    {

                        if (!roleManager.RoleExists(RolesConst.Pai))
                        {
                            var role = new IdentityRole();
                            role.Name = RolesConst.Pai;
                            roleManager.Create(role);
                        }

                        result = UserManager.AddToRole(user.Id, RolesConst.Pai);

                        if (result.Succeeded)
                        {
                            try
                            {
                                Pai p = new Pai();

                                p.Cidade = _db.Cidades.FirstOrDefault(c => c.CidadeID == pai.Cidade.CidadeID);
                                p.UserID = user.Id;
                                p.Nome = pai.Nome;
                                p.Morada = pai.Morada;
                                p.CodPostal = pai.CodPostal;
                                p.Telefone = pai.Telefone;

                                _db.Pais.Add(p);
                                _db.SaveChanges();

                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                                return RedirectToAction("Index");
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                //RollBack 
                                UserManager.RemoveFromRole(user.Id, RolesConst.Pai);
                                result = UserManager.Delete(user);
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        ModelState.AddModelError("", "\n" + string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage));

                                        // raise a new exception nesting
                                        // the current instance as InnerException

                                    }
                                }
                                return View();
                            }

                        }
                        else
                            UserManager.Delete(user); //RollBack Role
                    }
                    else
                        AddErrors(result);
                }

                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: Pais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("Index");

            Pai p = new Pai();
            p = _db.Pais.FirstOrDefault(fs => fs.PaisID == id);

            PaiViewModelEdit pai = new PaiViewModelEdit();
            pai.Morada = p.Morada;
            pai.CodPostal = p.CodPostal;
            pai.Telefone = p.Telefone;
            pai.Nome = p.Nome;
            pai.Cidade = p.Cidade;
            pai.Cidades = _db.Cidades.ToList();

            return View(pai);
        }

        // POST: Pais/Edit/5
        [HttpPost]
        public ActionResult Edit(PaiViewModelEdit pai)
        {
            try
            {
                pai.Cidades = _db.Cidades.ToList();
                if (ModelState.IsValid)
                {
                    var Uid = User.Identity.GetUserId();

                    var p = _db.Pais.FirstOrDefault(fs => fs.UserID == Uid);

                    p.Morada = pai.Morada;
                    p.Nome = pai.Nome;
                    p.Telefone = pai.Telefone;
                    p.CodPostal = pai.CodPostal;
                    p.Cidade = _db.Cidades.FirstOrDefault(c => c.CidadeID == pai.Cidade.CidadeID);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(pai);
                // TODO: Add update logic here


            }
            catch
            {
                return View(pai);
            }
        }

        // GET: Pais/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pais/Delete/5
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult Avaliacao(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("Index");
            ListaInst(Convert.ToInt32(id));
            return View();
        }
        [HttpPost]
        public ActionResult Avaliacao(AvaliacaoViewModelAdd av)
        {
            var Uid = User.Identity.GetUserId();
            var id = _db.Pais.Where(w => w.UserID == Uid).Select(s => s.PaisID).Single();
            ListaInst(Convert.ToInt32(id));
            try
            {
                if (ModelState.IsValid)
                {
                    Avaliacao a = new Avaliacao();
                    a.Pais = _db.Pais.Find(Convert.ToInt32(id));
                    a.Data = DateTime.Now;
                    a.Instituicoes = _db.Instituicoes.Find(av.InstituicoesID);
                    a.Nota = av.Nota;
                    a.Descricao = av.Descricao;
                    _db.Avaliacoes.Add(a);
                    _db.SaveChanges();
                    return RedirectToAction("Index");

                }

                return View();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                //RollBack 
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError("", "\n" + string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage));

                        // raise a new exception nesting
                        // the current instance as InnerException

                    }
                }
                return View();
            }
            catch (Exception)
            {

            }
            return View();
        }
        void ListaInst(int id)
        {
            var anoP = DateTime.Now.AddYears(-1);
            // vai buscar todas as instituicoes que ja acabaram o ano lectivo
            var finalAno = _db.Actividades.Where(w => w.Descricao.ToLower().Equals("ano lectivo")
                            && w.DataInicio.Year == anoP.Year
                            && w.DataTermino <= DateTime.Now).ToList();

            var Avaliacoes = _db.Avaliacoes.Where(w => w.Data.Year == DateTime.Now.Year && w.PaisID == id).ToList();

            foreach (var it in Avaliacoes)
            {
                finalAno.Remove(finalAno.FirstOrDefault(fs => fs.Instituicao.InstituicaoID == it.InstituicoesID));
            }

            var inst = finalAno.Select(s => s.Instituicao).ToList();
            if (finalAno.Count == 0)
                ViewBag.Inst = null;
            else
                ViewBag.Inst = new SelectList(finalAno.Select(s => s.Instituicao).ToList(), "InstituicaoId", "Nome");
        }

        public ActionResult LstIns(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("Index");
            var aux = _db.PaisInstituiçoes.Where(w => w.PaisID == id).ToList();
            var lstInst = _db.Instituicoes.ToList();
            foreach (var it in aux)
            {
                lstInst.Remove(lstInst.FirstOrDefault(fs => fs.InstituicaoID == it.InstituicoesID));
            }
            ViewBag.idP = id;
            return View(lstInst);
        }
        public ActionResult PedirEntrar(int? idP, int? idI)
        {
            if (idP == null || idP <= 0 || idI == null || idI <= 0)
                return RedirectToAction("Index");
            try
            {
                PaiInstituicao pi = new PaiInstituicao();
                pi.Data = DateTime.Now;
                pi.InstituicoesID = Convert.ToInt32(idI);
                pi.PaisID = Convert.ToInt32(idP);
                pi.Activo = false;
                _db.PaisInstituiçoes.Add(pi);
                _db.SaveChanges();

            }
            catch
            {

            }
            return RedirectToAction("LstIns", "Pais", new { id = idP });
        }

        public ActionResult LstActividades(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("Index");
            ViewBag.Id = id;
            return View(_db.PaisInstituiçoes.Where(w => w.PaisID == id && w.Activo).Select(s => s.Instituicoes).ToList());
        }


        public ActionResult InfoActividades(int? idI, int? idP)
        {
            if (idP == null || idP <= 0 || idI == null || idI <= 0)
                return RedirectToAction("Index");
            ViewBag.Id = idP;
            return View(_db.Actividades.Where(w => w.Instituicao.InstituicaoID == idI).ToList());
        }


        public ActionResult HistAvaliacoes(int? id)
        {
           if (id == null || id <= 0)
                return RedirectToAction("Index");
            return View(_db.Avaliacoes.Where(w => w.Pais.PaisID == id).OrderByDescending(ob => ob.Data ).ToList());
        }
    }
}
