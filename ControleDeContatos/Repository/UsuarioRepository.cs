using System;
using System.Collections.Generic;
using System.Linq;

using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repository
{
    public class UsuarioRepository : IUsuarioServices
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepository()
        {
            _bancoContext = new BancoContext();
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x =>
                x.Login.ToUpper() == login.ToUpper() && x.Email.ToUpper() == email.ToUpper());
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

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = ListarPorId(usuario.Id);

            if (usuarioDb == null) return null;

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Login = usuario.Login;
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDb);
            _bancoContext.SaveChanges();

            return usuarioDb;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenha)
        {
            UsuarioModel usuarioDB = ListarPorId(alterarSenha.Id);

            if (usuarioDB == null) throw new Exception("Usuário não encontrado!");

            if (!usuarioDB.SenhaValida(alterarSenha.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDB.SenhaValida(alterarSenha.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

            usuarioDB.SetNovaSenha(alterarSenha.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
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
