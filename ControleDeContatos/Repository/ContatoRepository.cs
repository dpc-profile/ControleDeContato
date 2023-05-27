using System;
using System.Collections.Generic;
using System.Linq;

using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepository()
        {
            _bancoContext = new BancoContext();
        }

        public void Adicionar(ContatoModel contato)
        {
            try
            {
                _bancoContext.Contatos.Add(contato);
                _bancoContext.SaveChanges();
            }
            catch (System.Exception)
            {
               throw new Exception("Erro ao adicionar o contato do banco de dados");
            }
        }

        public void Apagar(ContatoModel contatoModel)
        {
            try
            {
                _bancoContext.Contatos.Remove(contatoModel);
                _bancoContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new Exception("Erro ao apagar o contato do banco de dados");
            }

        }

        public void Atualizar(ContatoModel contato)
        {
            try
            {
                _bancoContext.Contatos.Update(contato);
                _bancoContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new Exception("Erro ao atualizar o contato no banco de dados");
            }

        }

        public List<ContatoModel> BuscarTodos(int usuarioId)
        {
            return _bancoContext.Contatos.Where(x => x.UsuarioId == usuarioId).ToList();
        }

        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }
    }
}
