using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Services.Interfaces;

namespace ControleDeContatos.Services
{
    public class ContatoServices : IContatoServices
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoServices(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public void ApagarContato(int id)
        {
            ContatoModel contatoDb = _contatoRepository.ListarPorId(id);

            if (contatoDb == null) throw new Exception("Contato não existe");

            _contatoRepository.Apagar(contatoDb);

        }

        public void AtualizarContato(ContatoModel contato)
        {
            ContatoModel contatoDb = _contatoRepository.ListarPorId(contato.Id);

            if (contatoDb == null) throw new Exception("Contato não existe");

            contatoDb.Nome = contato.Nome;
            contatoDb.Email = contato.Email;
            contatoDb.Celular = contato.Celular;

            _contatoRepository.Atualizar(contatoDb);
        }

        public List<ContatoModel> BuscarContatos(int id)
        {
            var contatosDb = _contatoRepository.BuscarTodos(id);

            if (contatosDb == null) throw new Exception("Não existe contatos");

            return contatosDb;

        }

        public ContatoModel BuscarContato(int id)
        {
            ContatoModel contatoDb = _contatoRepository.ListarPorId(id);

            if (contatoDb == null) throw new Exception("Contato não existe");

            return contatoDb;

        }
        public void CriarContato(ContatoModel contato)
        {
            _contatoRepository.Adicionar(contato);

        }
    }
}