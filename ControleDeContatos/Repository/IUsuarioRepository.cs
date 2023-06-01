using System;
using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel BuscarPorLogin(string login);
        
        UsuarioModel BuscarPorEmail(string email);
                
        UsuarioModel BuscarPorEmailELogin(string email, string login);

        List<UsuarioModel> BuscarTodos();

        UsuarioModel ListarPorId(int id);
        
        void Adicionar(UsuarioModel usuario);

        void Atualizar(UsuarioModel usuario);
        
        void Apagar(UsuarioModel usuario);
    }
}
