using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Repository;
using ControleDeContatos.Models;

using Xunit;
using Moq;
using ControleDeContatos.Tests.Tests.Controllers;

namespace ControleDeContatos.Tests.Tests.Repository
{
    public class ContatoRepositoryTests : IDisposable
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public ContatoRepositoryTests()
        {
            _contatoRepository = new ContatoRepository();
            _usuarioRepository = new UsuarioRepository();

            _contatoRepository.CreateSavepointAsync();

            OrganizarPreTeste();
        }

        public void Dispose()
        {
            Setup_Reverter();

        }

        [Fact]
        public void TestarBuscarTodos()
        {
            // Act
            // Passa um id de usuarios, associado aos contatos
            List<ContatoModel> result = _contatoRepository.BuscarTodos(
                fakeUsuario.UsuarioModelParaContatos_Database().Id);

            // Assert
            Assert.IsType<List<ContatoModel>>(result);
            Assert.True(result.Count == 3);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestarListarPorId(int id)
        {
            ContatoModel resultado = _contatoRepository.ListarPorId(id);

            Assert.Equal(id, resultado.Id);

        }

        [Fact]
        public void TestarListarPorId_IdInvalido()
        {
            ContatoModel resultado = _contatoRepository.ListarPorId(4);

            Assert.Null(resultado);

        }

        [Theory]
        [InlineData(1, "Carlos 1", "carlos1@teste.com")]
        [InlineData(2, "Carlos SuperTester", "carlos2@teste.com")]
        [InlineData(3, "Carlos UltraTester", "carlos3@teste.com")]
        public void TestarAtualizar(int id, string nome, string email)
        {
            // Arrange
            ContatoModel contatoDb = _contatoRepository.ListarPorId(id);

            contatoDb.Nome = nome;
            contatoDb.Email = email;
            contatoDb.Celular = "11 94325-1234";

            // Act
            _contatoRepository.Atualizar(contatoDb);

        }

        [Fact]
        public void TestarAtualizar_Exception()
        {
            // Assert
            var mensagem = Assert.Throws<Exception>(
                // Act
                () => _contatoRepository.Atualizar(It.IsAny<ContatoModel>())
            );
            Assert.Equal("Erro ao atualizar o contato no banco de dados", mensagem.Message);

        }

        [Fact]
        public void TestarAdicionar_Exception()
        {
            // Assert
            var mensagem = Assert.Throws<Exception>(
                // Act
                () => _contatoRepository.Adicionar(It.IsAny<ContatoModel>())
            );
            Assert.Equal("Erro ao adicionar o contato do banco de dados", mensagem.Message);

        }

        [Fact]
        public void TestarApagar()
        {
            // Arrange
            ContatoModel contatoDb = _contatoRepository.ListarPorId(3);

            // Act
            _contatoRepository.Apagar(contatoDb);
        }

        [Fact]
        public void TestarApagar_Exception()
        {
            // Assert
            var mensagem = Assert.Throws<Exception>(
                // Act
                () => _contatoRepository.Apagar(It.IsAny<ContatoModel>())
            );
            Assert.Equal("Erro ao apagar o contato do banco de dados", mensagem.Message);
        }

        private void OrganizarPreTeste()
        {

            // // Os contatos precisam de um usuario por conta da FOREING_KEY
            // _usuarioRepository.Adicionar(fakeUsuario.UsuarioModelParaContatos_Database());

            // Adicionar os contatos no bd
            foreach (var contato in fakeContato.VariosContatoModel_Database())
            {
                _contatoRepository.Adicionar(contato);
            }

        }

        private void Setup_Reverter()
        {
            _contatoRepository.RollbackAsync();
        }
    }

}

