using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Services.Interfaces
{
    public interface IUSuarioServices
    {
        List<UsuarioModel> BuscarUsuarios() ;
        UsuarioModel BuscarUsuario(int id);
        void AdicionarUsuario(UsuarioModel usuario);
        void AtualizarUsuario(UsuarioSemSenhaModel usuarioSemSenha);
        void ApagarUsuario(int id);
    }
}