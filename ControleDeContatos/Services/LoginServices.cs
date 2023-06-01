using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Services.Interfaces;

namespace ControleDeContatos.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void FazerLogin(LoginModel loginModel, ISessao sessao)
        {
            try
            {
                UsuarioModel usuario = _usuarioRepository.BuscarPorLogin(loginModel.Login);

                if (usuario == null) throw new UsuarioInvalidoException("Usuário inválido");

                if (!usuario.SenhaValida(loginModel.Senha)) throw new SenhaInvalidaException("Senha inválida");

                sessao.CriarSessaoUsuario(usuario);

            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}