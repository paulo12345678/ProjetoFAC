﻿using System;
using System.Collections.Generic;
using Model.Models;
using Model.Models.Exceptions;
using Persistencia.Persistence;

namespace Negocio.Business
{
    public class GerenciadorEmpresa
    {
        private RepositorioEmpresa persistencia;

        public GerenciadorEmpresa()
        {
            persistencia = new RepositorioEmpresa();
        }

        public Empresa Adicionar(Empresa empresa)
        {
            persistencia.Adicionar(empresa);
            return empresa;
        }

        public void Editar(Empresa empresa)
        {
            persistencia.editar(empresa);

        }

        public void Remover(Empresa empresa)
        {
            persistencia.remover(empresa);
        }
        public Empresa ObterByLoginSenha(string login, string senha)
        {
            return persistencia.Obter(e => e.Email.ToLowerInvariant().Equals(login.ToLowerInvariant()) &&
                e.Senha.ToLowerInvariant().Equals(senha.ToLowerInvariant()));
        }

        public bool BuscarPreCadastro(int? id, string email)
        {
            if (persistencia.Obter(e => e.Email.ToLowerInvariant().Equals(email.ToLowerInvariant()) &&
                 e.Id == id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public Empresa Obter(int? id)
        {
            return persistencia.Obter(u => u.Id == id);
        }

        public List<Empresa> ObterTodos()
        {
            return persistencia.ObterTodos();
        }
    }
}
