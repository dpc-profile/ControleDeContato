using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Repository;
using ControleDeContatos.Models;

using Xunit;
using Moq;

namespace ControleDeContatos.Tests.Tests.Repository
{
    public class ContatoRepositoryTests
    {
        private readonly IContatoRepository _repository;
        public ContatoRepositoryTests()
        {
            _repository = new ContatoRepository();
        }

        [Fact]
        public void TestarBuscarTodos()
        {
            // Act
            List<ContatoModel> lista = _repository.BuscarTodos(1);

            // Assert
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
        public void TestarListarPorId_IdInvalido()
        {
            ContatoModel resultado = _repository.ListarPorId(4);

            Assert.Null(resultado);

        }

        [Theory]
        [InlineData(1, "Carlos Tester1", "carlos1@teste.com")]
        [InlineData(2, "Carlos Tester2", "carlos2@teste.com")]
        [InlineData(3, "Carlos Tester3", "carlos3@teste.com")]
        public void TestarAtualizar(int id, string nome, string email)
        {
            // Arrange
            ContatoModel contatoNovasInfos = new ContatoModel()
            {
                Id = id,
                Nome = nome,
                Email = email,
                Celular = "11 94325-1234",
                UsuarioId = 1
            };

            // Act
            _repository.Atualizar(contatoNovasInfos);

        }

        [Fact]
        public void TestarAtualizar_Exception()
        {
            // Assert
            var mensagem = Assert.Throws<Exception>(
                // Act
                () => _repository.Atualizar(It.IsAny<ContatoModel>())
            );
            Assert.Equal("Erro ao atualizar o contato no banco de dados", mensagem.Message);

        }

        [Theory]
        [InlineData("Carlos MustBeDelete", "carl@teste.com")]
        public void TestarAdicionarEApagar(string nome, string email)
        {
            // Arrange
            ContatoModel contatoNovo = new ContatoModel()
            {
                Nome = nome,
                Email = email,
                Celular = "11 94325-1234",
                UsuarioId = 1
            };

            // Act Adicionar
            _repository.Adicionar(contatoNovo);

            // Act Apagar
            _repository.Apagar(contatoNovo);

        }

        [Fact]
        public void TestarAdicionar_Exception()
        {
            // Assert
            var mensagem = Assert.Throws<Exception>(
                // Act
                () => _repository.Adicionar(It.IsAny<ContatoModel>())
            );
            Assert.Equal("Erro ao adicionar o contato do banco de dados", mensagem.Message);
            
        }

        [Fact]
        public void TestarApagar_Exception()
        {
            // Assert
            var mensagem = Assert.Throws<Exception>(
                // Act
                () => _repository.Apagar(It.IsAny<ContatoModel>())
            );
            Assert.Equal("Erro ao apagar o contato do banco de dados", mensagem.Message);
        }

    }

}

