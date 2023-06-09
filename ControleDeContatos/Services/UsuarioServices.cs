using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Services.Interfaces;

using namesource.ControleDeContatos.Exceptions;

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
            if (_usuarioRepository.BuscarPorLogin(usuario.Login) != null) throw new LoginJaCadastradoException("Login já cadastrado");

            if (_usuarioRepository.BuscarPorEmail(usuario.Email) != null) throw new EmailJaCadastradoException("Email já cadastrado");

            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();

            _usuarioRepository.Adicionar(usuario);

        }

        public void ApagarUsuario(int id)
        {
            UsuarioModel usuarioDb = _usuarioRepository.ListarPorId(id);

            if (usuarioDb == null) throw new Exception("Usuário não existe");

            _usuarioRepository.Apagar(usuarioDb);

        }

        public void AtualizarUsuario(UsuarioSemSenhaModel usuarioSemSenha)
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

        public void AtualizarUsuarioComSenha(UsuarioModel usuarioModel)
        {
            UsuarioModel usuarioDb = _usuarioRepository.ListarPorId(usuarioModel.Id);

            if (usuarioDb == null) throw new UsuarioInvalidoException("Usuário não existe");

            _usuarioRepository.Atualizar(usuarioModel);
            
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