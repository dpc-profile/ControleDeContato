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
        private readonly IEmail _email;

        public LoginServices(IUsuarioRepository usuarioRepository, IEmail email)
        {
            _usuarioRepository = usuarioRepository;
            _email = email;
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

        public UsuarioModel ValidaUsuarioCadastrado(string email, string login)
        {
            try
            {
                UsuarioModel usuario = _usuarioRepository.BuscarPorEmail(email);

                if (usuario == null) throw new EmailNaoEncontradoException("O email informado não foi encontrado");

                if (usuario.Login != login) throw new LoginNaoEncontradoException("O login informado não foi encontrado");

                return usuario;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void EnviarNovaSenha(string email, string mensagem)
        {
            try
            {
                //De momento, o envio de email precisa ser configurado com um outlook valido
                _email.Enviar(email, "Sistema de Contatos - Nova senha", mensagem);

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}