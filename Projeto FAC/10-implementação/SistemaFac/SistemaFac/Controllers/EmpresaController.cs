
using System.Web.Mvc;
using Model.Models;
using Negocio.Business;
using System.Collections.Generic;


namespace SistemasFAC.Controllers
{
    public class EmpresaController : Controller
    {

        private GerenciadorEmpresa gerenciador;
        private Empresa empresa;

        public EmpresaController()
        {
            empresa = new Empresa() { Id = 1 };
            gerenciador = new GerenciadorEmpresa();
        }

        public ActionResult Index()
        {
            return View(gerenciador.ObterTodos());
        }

        public ActionResult Create()
        {
            ViewBag.ListaDescricao = new SelectList(gerenciador.ObterTodos());
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Empresa empresa = gerenciador.Obter(id);
                if (empresa != null)
                    return View(empresa);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Create(Empresa empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    gerenciador.Adicionar(empresa);
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
                Empresa empresa = gerenciador.Obter(id);
                return View(empresa);
            }
            return RedirectToAction("Index");

        }
        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(Empresa empresa) //FormCollection collection)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    
                    gerenciador.Editar(empresa);
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
                Empresa empresa = gerenciador.Obter(id);
                if (empresa != null)
                    return View(empresa);
            }
            return RedirectToAction("Index");
        }

        // POST: TipoServico/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Empresa empresa)
        {
            try
            {
                gerenciador.Remover(empresa);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Empresa");
            }

        }

    }
}