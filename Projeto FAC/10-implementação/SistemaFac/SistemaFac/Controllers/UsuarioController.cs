using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models.Account;
using Model.Models;
using Negocio.Business;
<<<<<<< remotes/origin/Codigo
using System.Web.Mvc;
using SistemaFac.Util;
=======
using SistemaFac.Util;
using System.Web.Security;
>>>>>>> local

namespace SistemaFac.Controllers
{
    public class UsuarioController : Controller
    {
        private GerenciadorUsuario gerenciador;

        public UsuarioController()
        {
            gerenciador = new GerenciadorUsuario();
        }

        public ActionResult Login ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel dadosLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
<<<<<<< remotes/origin/Codigo
                    gerenciador.Adicionar(usuario);
                    
                    SessionHelper.Set(SessionKeys.USUARIO, usuario);
                    return RedirectToAction("Index");
=======
                    // Obtendo o usuário.
                   // dadosLogin.Senha = Criptografia.GerarHashSenha(dadosLogin.Login + dadosLogin.Senha);
                    Usuario usuario = gerenciador.ObterByLoginSenha(dadosLogin.Login, dadosLogin.Senha);

                    // Autenticando.
                    if (usuario != null)
                    {
                        FormsAuthentication.SetAuthCookie(usuario.Login, dadosLogin.LembrarMe);
                        SessionHelper.Set(SessionKeys.USUARIO, usuario);

                        if (usuario.TipoUsuario == (int)Util.TipoUsuario.USUARIO) 
                            return RedirectToAction("IndexUsuario");
                        else if (usuario.TipoUsuario == (int)Util.TipoUsuario.ADMINISTRADOR)
                            return RedirectToAction("IndexADMINISTRADOR");
                        else if (usuario.TipoUsuario == (int)Util.TipoUsuario.EMPRESA)
                            return RedirectToAction("IndexEMPRESA");
                        else
                            return RedirectToAction(" Index ", " Administrador ");
                    }
>>>>>>> local
                }
                ModelState.AddModelError("", "Usuário e/ou senha inválidos.");
            }
            catch
            {
                ModelState.AddModelError("", "A autenticação falhou. Forneça informações válidas e tente novamente.");
            }
            // Se ocorrer algum erro, reexibe o formulário.
            return View();
        }
        [Authenticated]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
            }
            return RedirectToAction("Index", "Home");
        }

        [Authenticated]
        [CustomAuthorize(NivelAcesso = Util.TipoUsuario.USUARIO, MetodoAcao = "IndexADMINISTRADOR", Controladora = "Usuario")]
        public ActionResult IndexUsuario()
        {
            return View();
        }

        [Authenticated]
        [CustomAuthorize(NivelAcesso = Util.TipoUsuario.ADMINISTRADOR, MetodoAcao = "IndexEMPRESA", Controladora = "Usuario")]
        public ActionResult IndexADMINISTRADOR()
        {
            return View();
        }
        [Authenticated]
        [CustomAuthorize(NivelAcesso = Util.TipoUsuario.EMPRESA, MetodoAcao = "IndexADMINISTRADOR", Controladora = "Usuario")]
        public ActionResult IndexEMPRESA()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            ViewBag.TipoUsuario = new SelectList(gerenciador.ObterTodos(), "Id", "Descricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    collection["Senha"] = Criptografia.GerarHashSenha(collection["Login"] + collection["Senha"]);
                    Usuario usuario = new Usuario();
                    TryUpdateModel<Usuario>(usuario, collection.ToValueProvider());
                    if (gerenciador.BuscarPreCadastro(usuario.Id,usuario.Email))
                    {
                        Usuario auxiliar = usuario;
                        auxiliar.Id = gerenciador.Obter(usuario.Id).Id;
                        gerenciador.Editar(auxiliar);
                    }
                   // gerenciador.Adicionar(usuario);
                    return RedirectToAction("Login");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Index()
        {
            return View(gerenciador.ObterTodos());
        }
    }
}