using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Services.Interfaces
{
    public interface IUSuarioServices
    {
        UsuarioModel BuscarUsuarioPorLogin(string login);
        UsuarioModel BuscarUsuarioPorEmail(string email);
        List<UsuarioModel> BuscarUsuarios() ;
        UsuarioModel BuscarUsuario(int id);
        void AdicionarUsuario(UsuarioModel usuario);
        void AtualizarUsuario(UsuarioModel usuario);
        void EditarUsuario(UsuarioModel usuario);
        void ApagarUsuario(int id);
    }
}