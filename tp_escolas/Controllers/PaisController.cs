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
          Pai p = new Pai();
           if(User.Identity.IsAuthenticated)
            {
                
               string uId = User.Identity.GetUserId(); 
                p.Nome = _db.Pais.Where(u => u.UserID == uId).Select(u => u.Nome).FirstOrDefault().ToString();
                if (Roles.IsUserInRole("Pais"))
                    p.Morada = "Pais";
            }
            return View(p);
        }

        //Get: Pais/Registo
        public ActionResult Registo()
        {
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
        public async System.Threading.Tasks.Task<ActionResult> Registo(Pai pai)
        {
            try
            {

                ViewBag.CidadeID = new SelectList(_db.Cidades, "CidadeID", "CidadeNome");


                if (_UserDb.Users.Any(y => y.Email == pai.Email))
                {
                    ModelState.AddModelError("Email", "Conta já existente!");

                    return View();
                }
                if (pai.Cidades.CidadeID.ToString() == "")
                {
                    return View();
                }

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
                            pai.Cidades = _db.Cidades.FirstOrDefault(c => c.CidadeID == pai.Cidades.CidadeID);
                            pai.UserID = user.Id;
                            _db.Pais.Add(pai);
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

                    }else
                         UserManager.Delete(user); //RollBack Role
                }
                AddErrors(result);
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: Pais/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pais/Edit/5
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
    }
}
