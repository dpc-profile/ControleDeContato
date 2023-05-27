using System;
using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Repository
{
    public interface IContatoRepository
    {

        ContatoModel ListarPorId(int id);
        
        List<ContatoModel> BuscarTodos(int usuarioId);

        void Adicionar(ContatoModel contato);

        void Atualizar(ContatoModel contato);

        void Apagar(ContatoModel contatoModel);

        
    }
}
