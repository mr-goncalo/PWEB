using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tp_escolas.Models;
using tp_escolas.Models.ViewModels;

namespace tp_escolas.Controllers
{
    [Authorize(Roles = RolesConst.Admin)]
    public class AdminController : Controller
    {
        private EscolaContext _db;
        private ApplicationDbContext _UserDb;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
            _db = new EscolaContext();
            _UserDb = new ApplicationDbContext();

        }
        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        public ActionResult InstituicaoEditDel()
        {
            return View(_db.Instituicoes.ToList());
        }
        public ActionResult InstDel(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                    return RedirectToAction("InstituicaoEditDel");
                var inst = _db.Instituicoes.Find(id);
                var user = UserManager.FindById(inst.UserID);
                _db.Instituicoes.Remove(inst);
                _db.SaveChanges();

                var result = UserManager.Delete(user);

            }
            catch { }
            return RedirectToAction("InstituicaoEditDel");
        }

        public ActionResult InstEdit(int? id)
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
            IVm.InstituicaoID = inst.InstituicaoID;
            IVm.Morada = inst.Morada;
            IVm.CodPostal = inst.CodPostal;
            IVm.Telefone = inst.Telefone;
            IVm.TipoInstituicao = inst.TipoInstituicao;

            return View(IVm);
        }

        // POST: Instituicao/Edit/5
        [HttpPost]
        public ActionResult InstEdit(InstituicaoViewModelEdit i)
        {
            try
            {
                i.Cidades = _db.Cidades.ToList();


                if (ModelState.IsValid)
                {

                    i.Cidade = _db.Cidades.FirstOrDefault(c => c.CidadeID == i.Cidade.CidadeID);

                    var inst = _db.Instituicoes.Find(i.InstituicaoID);

                    inst.Morada = i.Morada;
                    inst.Nome = i.Nome;
                    inst.Telefone = i.Telefone;
                    inst.Cidade = i.Cidade;
                    inst.CodPostal = i.CodPostal;
                    inst.TipoInstituicao = i.TipoInstituicao;

                    _db.SaveChanges();

                    var lstServ = _db.InstituicoesServicos.Where(w => w.InstituicoesID == i.InstituicaoID);
                    _db.InstituicoesServicos.RemoveRange(lstServ);

                    foreach (var it in i.Servicos)
                    {
                        if (it.IsSelected)
                        {
                            var instSer = new InstituicaoServico();
                            instSer.InstituicoesID = i.InstituicaoID;
                            instSer.ServicosID = it.ServicosID;
                            _db.InstituicoesServicos.Add(instSer);
                        }

                    }

                    var lstTE = _db.InstituicaoTipoEnsino.Where(w => w.InstituicoesID == i.InstituicaoID);
                    _db.InstituicaoTipoEnsino.RemoveRange(lstTE);

                    foreach (var it in i.TiposEnsino)
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
        public ActionResult InstAdd()
        {

            InstituicaoViewModelAdd i = new InstituicaoViewModelAdd
            {
                Servicos = _db.Servicos.ToList(),
                Cidades = _db.Cidades.ToList(),
                TiposEnsino = _db.TipoEnsino.ToList()
            };

            return View(i);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> InstAdd(InstituicaoViewModelAdd inst)
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult PaisEditDel()
        {
            return View(_db.Pais.ToList());
        }

        public ActionResult PaiDel(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                    return RedirectToAction("PaisEditDel");
                var pai = _db.Pais.Find(id);
                var user = UserManager.FindById(pai.UserID);
                _db.Pais.Remove(pai);
                _db.SaveChanges();

                var result = UserManager.Delete(user);

            }
            catch { }
            return RedirectToAction("PaisEditDel");
        }

        public ActionResult PaiAdd()
        {

            ViewBag.CidadeID = new SelectList(_db.Cidades, "CidadeID", "CidadeNome");
            return View();
        }

        // POST: Pais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> PaiAdd(PaiViewModelAdd pai)
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

                                return RedirectToAction("PaisEditDel");
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
        public ActionResult PaiEdit(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("PaisEditDel");

            Pai p = new Pai();
            p = _db.Pais.FirstOrDefault(fs => fs.PaisID == id);

            PaiViewModelEdit pai = new PaiViewModelEdit();
            pai.Morada = p.Morada;
            pai.CodPostal = p.CodPostal;
            pai.Telefone = p.Telefone;
            pai.Nome = p.Nome;
            pai.Cidade = p.Cidade;
            pai.PaisID = p.PaisID;
            pai.Cidades = _db.Cidades.ToList();

            return View(pai);
        }

        // POST: Pais/Edit/5
        [HttpPost]
        public ActionResult PaiEdit(PaiViewModelEdit pai)
        {
            try
            {
                pai.Cidades = _db.Cidades.ToList();
                if (ModelState.IsValid)
                {


                    var p = _db.Pais.Find(pai.PaisID);

                    p.Morada = pai.Morada;
                    p.Nome = pai.Nome;
                    p.Telefone = pai.Telefone;
                    p.CodPostal = pai.CodPostal;
                    p.Cidade = _db.Cidades.FirstOrDefault(c => c.CidadeID == pai.Cidade.CidadeID);
                    _db.SaveChanges();
                    return RedirectToAction("PaisEditDel");
                }
                return View(pai);
                // TODO: Add update logic here


            }
            catch
            {
                return View(pai);
            }
        }
        public ActionResult AdminAddDel()
        {
            
            var userId = User.Identity.GetUserId();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var role = roleManager.FindByName(RolesConst.Admin).Users.First();
            var usersInRole = _UserDb.Users.Where(u => u.Id!= userId && u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();
             
            return View(usersInRole);
        }

        public ActionResult AdminAdd()
        {
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AdminAdd(AdminViewModel ad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_UserDb));
                    var user = new ApplicationUser { UserName = ad.Email, Email = ad.Email };
                    var result = await UserManager.CreateAsync(user, ad.Password);

                    if (result.Succeeded)
                    {

                        if (!roleManager.RoleExists(RolesConst.Admin))
                        {
                            var role = new IdentityRole();
                            role.Name = RolesConst.Admin;
                            roleManager.Create(role);
                        }

                        result = UserManager.AddToRole(user.Id, RolesConst.Admin);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("AdminAddDel");
                        }
                        else
                        {
                            UserManager.Delete(user);
                        }
                    }
                }
                return View();
            }
            catch (Exception)
            {


            }
            return View();
        }
        public ActionResult AdminDel(string id)
        {
            if (id == null || id == "")
                return RedirectToAction("AdminAddDel");

            var user = UserManager.FindById(id);
            UserManager.Delete(user);
            return RedirectToAction("AdminAddDel");
        }

        public ActionResult CidadesAddEdit()
        {
            return View(_db.Cidades.ToList());
        }
        public ActionResult CidadeEdit(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("CidadesAddEdit");
            return View(_db.Cidades.Find(id));
        }
        [HttpPost]
        public ActionResult CidadeEdit(Cidade cid)
        {
            try
            { 
                if (cid.CidadeNome.TrimStart() == "" || cid.CidadeNome == null)
                    ModelState.AddModelError("Descricao", "Por favor insira uma descrição");
                if (ModelState.IsValid)
                {
                    var c = _db.Cidades.Find(cid.CidadeID);
                    c.CidadeNome = cid.CidadeNome;
                    _db.SaveChanges();
                    return RedirectToAction("CidadesAddEdit");
                }
            }
            catch
            {

            }
            return RedirectToAction("CidadesAddEdit");
        }
        public ActionResult CidadeDel(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                    return RedirectToAction("CidadesAddEdit");
                _db.Cidades.Remove(_db.Cidades.Find(id));
                _db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("CidadesAddEdit");
        }
        public ActionResult CidadeAdd()
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult CidadeAdd(Cidade c)
        {
            try
            {
                // TODO: Add delete logic here
                if (c.CidadeNome.TrimStart() == "" || c.CidadeNome == null)
                    ModelState.AddModelError("Descricao", "Por favor insira uma descrição");
                if (ModelState.IsValid)
                {
                    _db.Cidades.Add(c);
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
    }
}
