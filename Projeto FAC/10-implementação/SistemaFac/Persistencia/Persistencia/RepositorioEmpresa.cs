using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;

namespace Persistencia.Persistence
{
    class repositorioEmpresa
    {
        private static List<Empresa> ListaEmpresa;

        static repositorioEmpresa()
        {
            ListaEmpresa = new List<Empresa>();
        }

        public Empresa Adicionar(Empresa empresa )
        {
            empresa.Id = ListaEmpresa.Count + 1;
            ListaEmpresa.Add(empresa);
            return empresa;
        }

        public void editar(Empresa empresa)
        {
            int posicao = ListaEmpresa.FindIndex(e=> e.Id ==empresa.Id);
            ListaEmpresa[posicao] = empresa;
        }

        public void remover(Empresa empresa)
        {
            int posicao = ListaEmpresa.FindIndex(e => e.Id == empresa.Id);
            ListaEmpresa.RemoveAt(posicao);
        }

        public Empresa obter(Func<Empresa , bool> where)
        {
            return ListaEmpresa.Where(where).FirstOrDefault();
        }

        public List<Empresa> Obtertodas()
        {
            return ListaEmpresa;
        }
    }
}
