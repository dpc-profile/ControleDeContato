using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;

namespace ControleDeContatos.Services
{
    public interface ISessao
    {
        void CriarSessaoUsuario(UsuarioModel usuario);
        UsuarioModel BuscarSessaoUsuario();
        void RemoverSessaoUsuario();
    }
}