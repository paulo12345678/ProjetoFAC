﻿using System.Web.Mvc;
using Model.Models;
using Negocio.Business;
using System.Collections.Generic;


namespace SistemasFAC.Controllers
{
    public class TipoEventoController : Controller
    {

        private GerenciadorTipoEventos gerenciador;
        private TipoEvento servico;

        public TipoEventoController()
        {
            servico = new TipoEvento() { Id = 1 };
            gerenciador = new GerenciadorTipoEventos();
        }

        public ActionResult Index()
        {
            return View(gerenciador.ObterTodos());
        }

        public ActionResult Create()
        {
            ViewBag.ListaDescricao = new SelectList(gerenciador.ObterTodos(), "Descricao");
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                TipoEvento tipoEvento = gerenciador.Obter(id);
                if (tipoEvento != null)
                    return View(tipoEvento);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Create(TipoEvento tipoevento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    gerenciador.Adicionar(tipoevento);
                    return RedirectToAction("Index");
                }
            }
            catch
            {

            }
            return View();
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id.HasValue)
            {
                TipoEvento tipoEvento = gerenciador.Obter(id);
                return View(tipoEvento);
            }
            return RedirectToAction("Index");

        }
        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(TipoEvento tipoEvento) //FormCollection collection)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //Usuario usuario = new Usuario();
                    //TryUpdateModel<Usuario>(usuario, collection.ToValueProvider());
                    gerenciador.Editar(tipoEvento);
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                TipoEvento tipoEvento = gerenciador.Obter(id);
                if (tipoEvento != null)
                    return View(tipoEvento);
            }
            return RedirectToAction("Index");
        }

        // POST: TipoServico/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TipoEvento tipoEvento)
        {
            try
            {
                gerenciador.Remover(tipoEvento);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("TipoEvento");
            }

        }

    }
}