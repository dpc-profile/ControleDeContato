using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Data;
using ControleDeContatos.Models;

using Microsoft.EntityFrameworkCore.Storage;

namespace ControleDeContatos.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BancoContext _bancoContext;
        private IDbContextTransaction _transaction;

        public UsuarioRepository()
        {
            _bancoContext = new BancoContext();
        }

        public async Task CreateSavepointAsync()
        {
            _transaction = _bancoContext.Database.BeginTransaction();
            await _transaction.CreateSavepointAsync("AntesTestesUsuario");
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackToSavepointAsync("AntesTestesUsuario");
            await _transaction.ReleaseSavepointAsync("AntesTestesUsuario");
        }

        
        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }
        public UsuarioModel BuscarPorEmail(string email)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
        }

        public void Adicionar(UsuarioModel usuario)
        {
            try
            {
                _bancoContext.Usuarios.Add(usuario);
                _bancoContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new Exception("Erro ao adicionar o usuário do banco de dados");
            }

        }

        public void Apagar(UsuarioModel usuario)
        {
            try
            {
                _bancoContext.Usuarios.Remove(usuario);
                _bancoContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new Exception("Erro ao apagar o usuário do banco de dados");
            }

        }

        public void Atualizar(UsuarioModel usuario)
        {
            try
            {
                _bancoContext.Usuarios.Update(usuario);
                _bancoContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new Exception("Erro ao atualizar o usuário no banco de dados");
            }

        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }


    }
}
