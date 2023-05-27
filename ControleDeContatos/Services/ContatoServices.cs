using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;

namespace ControleDeContatos.Services
{
    public class ContatoServices : IContatoServices
    {
        private readonly IContatoRepository _contatoRepository;

        public void ApagarContato(int id)
        {
            try
            {
                ContatoModel contatoDb = _contatoRepository.ListarPorId(id);

                if (contatoDb == null) throw new Exception("Contato não existe");

                _contatoRepository.Apagar(contatoDb);

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void AtualizarContato(ContatoModel contato)
        {
            try
            {
                ContatoModel contatoDb = _contatoRepository.ListarPorId(contato.Id);

                if (contatoDb == null) throw new Exception("Contato não existe");

                _contatoRepository.Atualizar(contatoDb);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public List<ContatoModel> BuscarContatos(int id)
        {
            try
            {
                List<ContatoModel> contatosDb = _contatoRepository.BuscarTodos(id);

                if (!contatosDb.Any()) throw new Exception("Contato não existe");

                return contatosDb;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public ContatoModel BuscarContato(int id)
        {
            try
            {
                ContatoModel contatoDb = _contatoRepository.ListarPorId(id);

                if (contatoDb == null) throw new Exception("Contato não existe");

                return contatoDb;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public void CriarContato(ContatoModel contato)
        {
            try
            {
                _contatoRepository.Adicionar(contato);

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}