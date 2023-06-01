using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;

namespace ControleDeContatos.Services.Interfaces
{
    public interface ILoginServices
    {
        public void FazerLogin(LoginModel loginModel, ISessao sessao);
        public UsuarioModel ValidaUsuarioCadastrado(string email, string login);
        public void EnviarNovaSenha(string email, string mensagem);
    }
}