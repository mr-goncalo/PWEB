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
using Microsoft.AspNet.Identity.EntityFramework;

namespace tp_escolas.Controllers
{
    public class InstituicaoController : Controller
    {
        private EscolaContext _db;
        private ApplicationDbContext _UserDb;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public InstituicaoController()
        {
            _db = new EscolaContext();
            _UserDb = new ApplicationDbContext();

        }
        public InstituicaoController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Instituicao
        [Authorize(Roles = "Instituição")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registo()
        {
            Instituicao i = new Instituicao
            {
                Servicos = _db.Servicos.ToList(),
                Cidades = _db.Cidades.ToList(),
                TiposEnsino = _db.TipoEnsino.ToList()
            };
            return View(i);
        }

        // GET: Instituicao/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        [AllowAnonymous]
        public ActionResult Info(int? id)
        {
            if (id == null || id == 0)
               return  RedirectToAction("ListaInstituicoes");
            Instituicao inst = new Instituicao();
            inst = _db.Instituicoes.Where(x => x.InstituicaoID == id).FirstOrDefault();

            inst.TiposEnsino = _db.TipoEnsino.Where(w => w.InstituicoesTipoEnsino.Any(s => s.TipoEnsinoID == w.TipoEnsinoID && s.InstituicoesID == id)).ToList();
            inst.Servicos = _db.Servicos.Where(w => w.InstituicoesServicos.Any(s => s.ServicosID == w.ServicosID && s.InstituicoesID == id) ).ToList();



            return View(inst);
        }

        // GET: Instituicao/Create
        [AllowAnonymous]
        public ActionResult ListaInstituicoes()
        {
            return View(_db.Instituicoes.ToList());
        }

        // POST: Instituicao/Registo
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Registo(Instituicao inst)
        {
            try
            {
                inst.Cidades = _db.Cidades.ToList();

                if (_UserDb.Users.Any(y => y.Email == inst.Email))
                {
                    ModelState.AddModelError("Email", "Conta já existente!");
                      
                }
                if (inst.Cidade.CidadeID.ToString() == "")
                {
                    ModelState.AddModelError("Cidade", "Selecione uma Cidade Valida!");
                  
                }
                if (inst.TipoInstituicao == 0)
                {
                    ModelState.AddModelError("TipoInstituicao", "Selecione uma Instituição!");
                    
                }
                int count = 0;
                foreach (var it in inst.TiposEnsino)
                    if (it.IsSelected)
                        count++;
                if (count == 0)
                {
                    ModelState.AddModelError("TiposEnsino", "Selecione Pelo menos 1 Serviço de ensino!");
                    
                }

                if (ModelState.IsValid)
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_UserDb));
                    var user = new ApplicationUser { UserName = inst.Email, Email = inst.Email };
                    var result = await UserManager.CreateAsync(user, inst.Password);

                    if (result.Succeeded)
                    {

                        if (!roleManager.RoleExists("Instituição"))
                        {
                            var role = new IdentityRole();
                            role.Name = "Instituição";
                            roleManager.Create(role);
                        }

                        result = UserManager.AddToRole(user.Id, "Instituição");

                        if (result.Succeeded)
                        {
                            try
                            {
                                inst.UserID = user.Id;
                                _db.Instituicoes.Add(inst);
                                int? auxId = _db.Instituicoes.Max(u => (int?)u.InstituicaoID) + 1;
                                if (auxId == null)
                                    auxId = 1;
                                inst.InstituicaoID = Convert.ToInt32(auxId); 
                                foreach (var it in inst.Servicos)
                                {
                                    if (it.IsSelected)
                                    {
                                        var instSer = new InstituicaoServico();
                                        instSer.InstituicoesID = inst.InstituicaoID;
                                        instSer.ServicosID = it.ServicosID;
                                        _db.InstituicoesServicos.Add(instSer);
                                    }

                                }

                                foreach (var it in inst.TiposEnsino)
                                {
                                    if (it.IsSelected)
                                    {
                                        var instTEnsi = new InstituicaoTipoEnsino();
                                        instTEnsi.InstituicoesID = inst.InstituicaoID;
                                        instTEnsi.TipoEnsinoID = it.TipoEnsinoID;
                                        _db.InstituicaoTipoEnsino.Add(instTEnsi);
                                    } 
                                }

                                _db.SaveChanges();
                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                                return RedirectToAction("Index");
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                //RollBack 
                                UserManager.RemoveFromRole(user.Id, "Pais");
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
                                return View(inst);
                            }

                        }
                        else
                            UserManager.Delete(user); //RollBack Role
                    }
                    AddErrors(result);
                }
                
                
                // TODO: Add insert logic here
                 
                return View(inst);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Instituicao/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Instituicao/Edit/5
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

        // GET: Instituicao/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Instituicao/Delete/5
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

    }
}
