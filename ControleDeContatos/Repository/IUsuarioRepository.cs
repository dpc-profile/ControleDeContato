using System;
using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel BuscarPorLogin(string login);
        
        UsuarioModel BuscarPorEmail(string email);
                
        List<UsuarioModel> BuscarTodos();

        UsuarioModel ListarPorId(int id);
        
        void Adicionar(UsuarioModel usuario);

        void Atualizar(UsuarioModel usuario);
        
        void Apagar(UsuarioModel usuario);
    }
}
