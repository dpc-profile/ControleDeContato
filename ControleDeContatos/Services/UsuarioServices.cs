using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Services.Interfaces;

namespace ControleDeContatos.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void AdicionarUsuario(UsuarioModel usuario)
        {
            try
            {
                if (_usuarioRepository.BuscarPorLogin(usuario.Login) != null) throw new Exception("Login já cadastrado");

                if (_usuarioRepository.BuscarPorEmail(usuario.Email) != null) throw new Exception("Email já cadastrado");
                
                usuario.DataCadastro = DateTime.Now;
                usuario.SetSenhaHash();

                _usuarioRepository.Adicionar(usuario);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void ApagarUsuario(int id)
        {
            try
            {
                UsuarioModel usuarioDb = _usuarioRepository.ListarPorId(id);

                if (usuarioDb == null) throw new Exception("Usuário não existe");

                _usuarioRepository.Apagar(usuarioDb);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void AtualizarUsuario(UsuarioSemSenhaModel usuarioSemSenha)
        {
            try
            {
                UsuarioModel usuario = new UsuarioModel()
                {
                    Id = usuarioSemSenha.Id,
                    Nome = usuarioSemSenha.Nome,
                    Email = usuarioSemSenha.Email,
                    Login = usuarioSemSenha.Login,
                    Perfil = usuarioSemSenha.Perfil
                };

                UsuarioModel usuarioDb = _usuarioRepository.ListarPorId(usuario.Id);

                if (usuarioDb == null) throw new Exception("Usuário não existe");

                usuarioDb.Nome = usuario.Nome;
                usuarioDb.Email = usuario.Email;
                usuarioDb.Login = usuario.Login;
                usuarioDb.Perfil = usuario.Perfil;
                usuarioDb.DataAtualizacao = DateTime.Now;

                _usuarioRepository.Atualizar(usuarioDb);
                
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public UsuarioModel BuscarUsuario(int id)
        {
            return _usuarioRepository.ListarPorId(id);
        }

        public List<UsuarioModel> BuscarUsuarios()
        {
            return _usuarioRepository.BuscarTodos();
        }
    }
}