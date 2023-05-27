using ControleDeContatos.Models;

namespace ControleDeContatos.Services.Interfaces
{
    public interface ISessao
    {
        void CriarSessaoUsuario(UsuarioModel usuario);
        UsuarioModel BuscarSessaoUsuario();
        void RemoverSessaoUsuario();
    }
}