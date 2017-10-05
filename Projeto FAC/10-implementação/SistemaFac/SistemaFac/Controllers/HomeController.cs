﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using SistemaFac.Util;
using Negocio.Business;
using Model.Models.Account;
using System.Web.Security;

namespace BibliotecaWeb.Controllers
{
    public class HomeController : Controller
    {
        private GerenciadorUsuario gerenciador;

        public HomeController()
        {
            gerenciador = new GerenciadorUsuario();
        }

        public ActionResult Index()
        {
            return View();
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

        public ActionResult Login()
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

                    // Obtendo o usuário.
                    // dadosLogin.Senha = Criptografia.GerarHashSenha(dadosLogin.Login + dadosLogin.Senha);
                    dadosLogin.Senha = Criptografia.GerarHashSenha(dadosLogin.Login + dadosLogin.Senha);
                    Usuario usuario = gerenciador.ObterByLoginSenha(dadosLogin.Login, dadosLogin.Senha);

                    // Autenticando.
                    if (usuario != null)
                    {
                        FormsAuthentication.SetAuthCookie(usuario.Login, dadosLogin.LembrarMe);
                        SessionHelper.Set(SessionKeys.USUARIO, usuario);

                        if (usuario.NvAcesso == (int)Util.TipoUsuario.USUARIO + 1)
                            return RedirectToAction("Index", "TipoEvento");
                        else if (usuario.NvAcesso == (int)Util.TipoUsuario.ADMINISTRADOR + 1)
                            return RedirectToAction("IndexADMINISTRADOR");
                        else if (usuario.NvAcesso == (int)Util.TipoUsuario.EMPRESA + 1)
                            return RedirectToAction("IndexEMPRESA");
                        else
                            return RedirectToAction(" Index ", " Administrador ");
                    }

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
    }
}