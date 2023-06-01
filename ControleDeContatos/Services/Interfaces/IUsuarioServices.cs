using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Services.Interfaces
{
    public interface IUsuarioServices
    {
        List<UsuarioModel> BuscarUsuarios() ;
        UsuarioModel BuscarUsuario(int id);
        void AdicionarUsuario(UsuarioModel usuario);
        void AtualizarUsuario(UsuarioSemSenhaModel usuarioSemSenha);
        void AtualizarUsuarioComSenha(UsuarioModel usuarioModel);
        void ApagarUsuario(int id);
    }
}