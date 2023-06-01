using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Services.Interfaces;

namespace ControleDeContatos.Services
{
    public class AlterarSenhaServices : IAlterarSenhaServices
    {
        private readonly IUsuarioServices _usuarioServices;
        public AlterarSenhaServices(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        public void AlterarSenha(AlterarSenhaModel alterarSenha)
        {
            try
            {
                UsuarioModel usuarioDB = _usuarioServices.BuscarUsuario(alterarSenha.Id);

                if (usuarioDB == null) throw new UsuarioInvalidoException("Usuário não encontrado!");

                if (!usuarioDB.SenhaValida(alterarSenha.SenhaAtual)) throw new SenhaNaoConfereException("Senha atual não confere!");

                if (usuarioDB.SenhaValida(alterarSenha.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

                usuarioDB.SetNovaSenha(alterarSenha.NovaSenha);
                usuarioDB.DataAtualizacao = DateTime.Now;

                _usuarioServices.AtualizarUsuarioComSenha(usuarioDB);
            }
            catch (System.Exception)
            {
                throw;
            }


        }
    }
}