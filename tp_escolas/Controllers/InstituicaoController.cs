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
using tp_escolas.Models.ViewModels;
using System.Globalization;

namespace tp_escolas.Controllers
{
    [Authorize(Roles = RolesConst.Instituicao)]
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

        public ActionResult Index()
        {
            var uId = User.Identity.GetUserId();
            ViewBag.Id = _db.Instituicoes.Where(u => u.UserID == uId).Select(u => u.InstituicaoID).FirstOrDefault();
            return View();
        }

        [AllowAnonymous]
        public ActionResult Registo()
        {
            InstituicaoViewModelAdd i = new InstituicaoViewModelAdd
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
                return RedirectToAction("ListaInstituicoes");
            InstituicaoViewModel inst = new InstituicaoViewModel();
            var i = _db.Instituicoes.Where(x => x.InstituicaoID == id).FirstOrDefault();

            inst.Cidade = i.Cidade;
            inst.CodPostal = i.CodPostal;
            inst.Morada = i.Morada;
            inst.Nome = i.Nome;
            inst.Telefone = i.Telefone;
            inst.TipoInstituicao = i.TipoInstituicao;
            
            inst.TiposEnsino = _db.TipoEnsino.Where(w => w.InstituicoesTipoEnsino.Any(s => s.TipoEnsinoID == w.TipoEnsinoID && s.InstituicoesID == id)).ToList();
            inst.Servicos = _db.Servicos.Where(w => w.InstituicoesServicos.Any(s => s.ServicosID == w.ServicosID && s.InstituicoesID == id) ).ToList();

            inst.Ites = _db.InstituicoesTipoEnsinoServicos.Where(w => w.InstituicoesID == id).ToList();

            var serv = _db.Servicos.ToList(); 
             
            foreach (var it in serv)
            {
                 inst.Ites.Where(w => w.ServicosID == it.ServicosID)
                    .Select(s => { s.Servicos = it; return s; }).ToList();
            }

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
        public async System.Threading.Tasks.Task<ActionResult> Registo(InstituicaoViewModelAdd inst)
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

                        if (!roleManager.RoleExists(RolesConst.Instituicao))
                        {
                            var role = new IdentityRole();
                            role.Name = RolesConst.Instituicao;
                            roleManager.Create(role);
                        }

                        result = UserManager.AddToRole(user.Id, RolesConst.Instituicao);

                        if (result.Succeeded)
                        {
                            try
                            {
                                Instituicao i = new Instituicao();

                                i.UserID = user.Id;
                                i.Cidade = _db.Cidades.FirstOrDefault(c => c.CidadeID == inst.Cidade.CidadeID);
                                i.Morada = inst.Morada;
                                i.Nome = inst.Nome;
                                i.CodPostal = inst.CodPostal;
                                i.Telefone = inst.Telefone;
                                i.TipoInstituicao = inst.TipoInstituicao;

                                _db.Instituicoes.Add(i);
                                _db.SaveChanges();


                                foreach (var it in inst.Servicos)
                                {
                                    if (it.IsSelected)
                                    {
                                        var instSer = new InstituicaoServico();
                                        instSer.InstituicoesID = i.InstituicaoID;
                                        instSer.ServicosID = it.ServicosID;
                                        _db.InstituicoesServicos.Add(instSer);
                                    }

                                }

                                foreach (var it in inst.TiposEnsino)
                                {
                                    if (it.IsSelected)
                                    {
                                        var instTEnsi = new InstituicaoTipoEnsino();
                                        instTEnsi.InstituicoesID = i.InstituicaoID;
                                        instTEnsi.TipoEnsinoID = it.TipoEnsinoID;
                                        instTEnsi.Valor = it.Valor;
                                        _db.InstituicaoTipoEnsino.Add(instTEnsi);
                                    }
                                }

                                _db.SaveChanges();
                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                                return RedirectToAction("Index");
                            }
                            catch (Exception e)
                            {
                                //RollBack 
                                UserManager.RemoveFromRole(user.Id, RolesConst.Instituicao);
                                result = UserManager.Delete(user);
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
                return View(inst);
            }
        }

        // GET: Instituicao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("Index");

            Instituicao inst = new Instituicao();
            InstituicaoViewModelEdit IVm = new InstituicaoViewModelEdit();
            inst = _db.Instituicoes.Where(x => x.InstituicaoID == id).FirstOrDefault();

            var TiposEnsinoSub = _db.InstituicaoTipoEnsino.Where(w => w.InstituicoesID == id).ToList();
            IVm.TiposEnsino = _db.TipoEnsino.ToList();


            var ServicosSub = _db.Servicos.Where(w => w.InstituicoesServicos.Any(s => s.ServicosID == w.ServicosID && s.InstituicoesID == id)).ToList();
            IVm.Servicos = _db.Servicos.ToList();
            IVm.Cidades = _db.Cidades.ToList();

            foreach (var serv in ServicosSub)
            {
                IVm.Servicos.Where(w => w.ServicosID == serv.ServicosID)
                    .Select(s => { s.IsSelected = true; return s; }).ToList();
            }

            foreach (var serv in TiposEnsinoSub)
            {
                IVm.TiposEnsino.Where(w => w.TipoEnsinoID == serv.TipoEnsinoID)
                    .Select(s => { s.IsSelected = true; s.Valor = serv.Valor; return s; }).ToList();
            }
            

            IVm.Nome = inst.Nome;
            IVm.Cidade = inst.Cidade;
            IVm.Morada = inst.Morada;
            IVm.CodPostal = inst.CodPostal;
            IVm.Telefone = inst.Telefone;
            IVm.TipoInstituicao = inst.TipoInstituicao;

            return View(IVm);
        }

        // POST: Instituicao/Edit/5
        [HttpPost]
        public ActionResult Edit(InstituicaoViewModelEdit i)
        {
            try
            {
                i.Cidades = _db.Cidades.ToList();


                if (ModelState.IsValid)
                {

                    var UserID = User.Identity.GetUserId();
                    var InstituicaoID = Convert.ToInt16(_db.Instituicoes.Where(w => w.UserID == UserID).Select(se => se.InstituicaoID).FirstOrDefault());
                    i.Cidade = _db.Cidades.FirstOrDefault(c => c.CidadeID == i.Cidade.CidadeID);

                    var inst = _db.Instituicoes.Find(InstituicaoID);

                    inst.Morada = i.Morada;
                    inst.Nome = i.Nome;
                    inst.Telefone = i.Telefone;
                    inst.Cidade = i.Cidade;
                    inst.CodPostal = i.CodPostal;
                    inst.TipoInstituicao = i.TipoInstituicao;

                    _db.SaveChanges();

                    var lstServ = _db.InstituicoesServicos.Where(w => w.InstituicoesID == InstituicaoID);
                    _db.InstituicoesServicos.RemoveRange(lstServ);

                    foreach (var it in i.Servicos)
                    {
                        if (it.IsSelected)
                        {
                            var instSer = new InstituicaoServico();
                            instSer.InstituicoesID = InstituicaoID;
                            instSer.ServicosID = it.ServicosID;
                            _db.InstituicoesServicos.Add(instSer);
                        }

                    }

                    var lstTE = _db.InstituicaoTipoEnsino.Where(w => w.InstituicoesID == InstituicaoID);
                    _db.InstituicaoTipoEnsino.RemoveRange(lstTE);

                    foreach (var it in i.TiposEnsino)
                    {
                        if (it.IsSelected)
                        {
                            var instTEnsi = new InstituicaoTipoEnsino();
                            instTEnsi.InstituicoesID = InstituicaoID;
                            instTEnsi.TipoEnsinoID = it.TipoEnsinoID;
                            instTEnsi.Valor = it.Valor;
                            _db.InstituicaoTipoEnsino.Add(instTEnsi);
                        }
                    }
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError("", "\n" + string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage));


                    }
                }
                return View(i);
            }
            catch (Exception e)
            {
                return View(i);
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

        public ActionResult ServicosLista(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("Index");

            InstituicaoViewModelEdit IVm = new InstituicaoViewModelEdit();


            var TiposEnsinoSub = _db.InstituicaoTipoEnsino.Where(w => w.InstituicoesID == id).ToList();
            var TiposEnsino = _db.TipoEnsino.ToList();

            foreach (var serv in TiposEnsinoSub)
            {
                TiposEnsino.Where(w => w.TipoEnsinoID == serv.TipoEnsinoID)
                   .Select(s => { s.IsSelected = true; s.Valor = serv.Valor; return s; }).ToList();
            }

            return View(TiposEnsino);
        }

        public ActionResult ServEnsino(int? id)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction("Index");
            }


            TipoEnsinoServicoViewModelAE tae = new TipoEnsinoServicoViewModelAE();
            TipoEnsino te = new TipoEnsino();
            te = _db.TipoEnsino.Find(id);

            tae.Descricao = te.Descricao;
            tae.TipoEnsinoID = te.TipoEnsinoID;

            var UserID = User.Identity.GetUserId();
            var InstituicaoID = Convert.ToInt16(_db.Instituicoes.Where(w => w.UserID == UserID).Select(se => se.InstituicaoID).FirstOrDefault());

            var ServicosSub = _db.Servicos.Where(w => w.InstituicoesTipoEnsinoServicos.Any(s => s.ServicosID == w.ServicosID && s.InstituicoesID == InstituicaoID && s.TipoEnsinoID == id)).ToList();
            tae.Servicos = _db.Servicos.ToList();

            foreach (var serv in ServicosSub)
            {
                tae.Servicos.Where(w => w.ServicosID == serv.ServicosID)
                      .Select(s => { s.IsSelected = true; return s; }).ToList();
            }
            return View(tae);
        }
        [HttpPost]
        public ActionResult ServEnsino(TipoEnsinoServicoViewModelAE tae)
        {
            try
            {
                var UserID = User.Identity.GetUserId();
                var InstituicaoID = Convert.ToInt16(_db.Instituicoes.Where(w => w.UserID == UserID).Select(se => se.InstituicaoID).FirstOrDefault());

                var lstServ = _db.InstituicoesTipoEnsinoServicos.Where(w => w.InstituicoesID == InstituicaoID && w.TipoEnsinoID == tae.TipoEnsinoID);
                _db.InstituicoesTipoEnsinoServicos.RemoveRange(lstServ);

                var TipoEnsino = _db.TipoEnsino.FirstOrDefault(fs => fs.TipoEnsinoID == tae.TipoEnsinoID);
                foreach (var it in tae.Servicos)
                {
                    if (it.IsSelected)
                    {
                        var instSer = new InstituicaoTipoEnsinoServico();
                        instSer.InstituicoesID = InstituicaoID;
                        instSer.ServicosID = it.ServicosID;
                        instSer.TipoEnsino = TipoEnsino;
                        _db.InstituicoesTipoEnsinoServicos.Add(instSer);
                    }

                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError("", "\n" + string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage));


                    }
                }
                return View(tae);
            }
            catch
            {
                return View(tae);
            }
        }

        public ActionResult ListaPais(int? idIns, int? idPai)
        {
            if (idIns == null || idIns <= 0)
                return RedirectToAction("Index");

            ViewBag.IdIns = idIns;

            if (idPai == null)
                return View(_db.Pais.Where(w => w.PaiInstituição.Any(a => a.InstituicoesID == idIns && a.Activo == false)).ToList());
            else
            {

                try
                {
                    var paiIns = _db.PaisInstituiçoes.Find(idPai, idIns);
                    paiIns.Activo = true;
                    _db.SaveChanges();
                    return View(_db.Pais.Where(w => w.PaiInstituição.Any(a => a.InstituicoesID == idIns && a.Activo == false)).ToList());
                }
                catch
                {
                    return View(_db.Pais.Where(w => w.PaiInstituição.Any(a => a.InstituicoesID == idIns && a.Activo == false)).ToList());

                }

            }
        }
        public ActionResult CriarActividade()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CriarActividade(Actividade act)
        {
            try
            {
                if (act.DataInicio < DateTime.Now)
                    ModelState.AddModelError("DataInicio", "A data de inico tem de ser maior que a Data de hoje!");
                if (act.DataInicio > act.DataTermino)
                    ModelState.AddModelError("DataInicio", "A data de inico tem de ser menor que a Data de Termino!");

                if (ModelState.IsValid)
                {
                    var UserId = User.Identity.GetUserId();
                    act.Instituicao = _db.Instituicoes.FirstOrDefault(fs => fs.UserID == UserId);

                    _db.Actividades.Add(act);
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
        public ActionResult ListaAct(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("Index");

            return View(_db.Actividades.Where(w => w.DataTermino >= DateTime.Now).ToList());
        }
        public ActionResult ActividadeEdit(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("ListaAct");
            var act = _db.Actividades.Find(id);
            
            return View(act);
        }
        [HttpPost]
        public ActionResult ActividadeEdit(Actividade act)
        {

            try
            {
                if (act.DataInicio < DateTime.Now)
                    ModelState.AddModelError("DataInicio", "A data de inico tem de ser maior que a Data de hoje!");
                if (act.DataInicio > act.DataTermino)
                    ModelState.AddModelError("DataInicio", "A data de inico tem de ser menor que a Data de Termino!");

                if (ModelState.IsValid)
                {
                    var UserId = User.Identity.GetUserId();
                    act.Instituicao = _db.Instituicoes.FirstOrDefault(fs => fs.UserID == UserId);
                    var actEdit = _db.Actividades.Find(act.ActividadeID);

                    actEdit.DataInicio = act.DataInicio;
                    actEdit.DataTermino = act.DataTermino;
                    actEdit.Descricao = act.Descricao;
                    actEdit.Instituicao = act.Instituicao;

                    _db.SaveChanges();
                    return RedirectToAction("ListaAct");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ActividadeDel(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("ListaAct");
            
            return View(_db.Actividades.Find(id));
        }
        [HttpPost]
        public ActionResult ActividadeDel(Actividade act)
        { 
            try
            {
                 _db.Actividades.Remove(_db.Actividades.Find(act.ActividadeID));
                _db.SaveChanges();
                return RedirectToAction("ListaAct");
            }
            catch
            {
                return RedirectToAction("ListaAct");
            }
            
        }

        public ActionResult Avaliacoes(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("Index");
             return View(_db.Avaliacoes.Where(w => w.InstituicoesID == id).ToList());
        }
    }

}
