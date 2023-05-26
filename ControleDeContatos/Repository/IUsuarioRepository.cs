using System;
using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel BuscarPorLogin(string login);
        
        UsuarioModel BuscarPorEmailELogin(string email, string login);

        List<UsuarioModel> BuscarTodos();

        UsuarioModel ListarPorId(int id);
        
        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Atualizar(UsuarioModel usuario);
        
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenha);

        bool Apagar(int id);
    }
}