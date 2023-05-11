using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Repository;
using ControleDeContatos.Models;

using Xunit;

namespace ControleDeContatos.Tests.Tests
{
    public class ContatosTests
    {
        private IContatoRepository _repository;

        public ContatosTests()
        {
            _repository = new ContatoRepository();
        }

        [Fact]
        public void TestarBuscarTodos()
        {
            List<ContatoModel> lista = _repository.BuscarTodos();

            Assert.NotNull(lista);
            Assert.True(lista.Count != 0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestarListarPorId(int id)
        {
            ContatoModel resultado = _repository.ListarPorId(id);

            Assert.Equal(id, resultado.Id);
        }

        [Fact]
        public void TestarInvalidoListarPorId()
        {
            ContatoModel resultado = _repository.ListarPorId(4);

            Assert.Null(resultado);
        }

        [Theory]
        [InlineData(1, "Carlos Tester1", "carlos1@teste.com")]
        [InlineData(2, "Carlos Tester2", "carlos2@teste.com")]
        [InlineData(3, "Carlos Tester3", "carlos3@teste.com")]
        public void TestartAtualizar(int id, string nome, string email)
        {
            ContatoModel contatoNovasInfos = new ContatoModel();
            contatoNovasInfos.Id = id;
            contatoNovasInfos.Nome = nome;
            contatoNovasInfos.Email = email;
            contatoNovasInfos.Celular = "11 94325-1234";

            ContatoModel resposta = _repository.Atualizar(contatoNovasInfos);
            Assert.NotNull(resposta);
        }
        
        [Theory]
        [InlineData("Carlos TesterNew", "carlosNew@teste.com")]
        public void TestartAdicionar(string nome, string email)
        {
            ContatoModel contatoNovo = new ContatoModel();
            contatoNovo.Nome = nome;
            contatoNovo.Email = email;
            contatoNovo.Celular = "11 94325-1234";

            ContatoModel resposta = _repository.Adicionar(contatoNovo);
            
            Assert.NotNull(resposta);
        }

        [Fact]
        public void TestarApagar()
        {
            ContatoModel contatoNovo = new ContatoModel();
            contatoNovo.Nome = "Carlos MustBeDelete";
            contatoNovo.Email = "carl@teste.com";
            contatoNovo.Celular = "11 94325-1234";

            ContatoModel respostaAdicionar = _repository.Adicionar(contatoNovo);

            bool respostaApagar = _repository.Apagar(respostaAdicionar.Id);

            Assert.True(respostaApagar);
        }
    }

}

