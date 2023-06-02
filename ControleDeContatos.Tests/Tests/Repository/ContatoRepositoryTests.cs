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
            OrganizarPreTeste();
        }

        public void Dispose()
        {
            LimparPosTeste();
        }

        [Fact]
        public void TestarBuscarTodos()
        {
            // Act
            // Passa um id de usuarios, associado aos contatos
            List<ContatoModel> result = _contatoRepository.BuscarTodos(fakeUsuario.UsuarioModel_Database().Id);

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
            try
            {
                // Os contatos precisam de um usuario por conta da FOREING_KEY
                _usuarioRepository.Adicionar(fakeUsuario.UsuarioModel_Database());

                // Adicionar os contatos no bd
                foreach (var contato in fakeContato.VariosContatoModel_Database())
                {
                    _contatoRepository.Adicionar(contato);
                }
            }
            catch (Exception e)
            {

            }

        }

        private void LimparPosTeste()
        {
            int usuarioId = fakeUsuario.UsuarioModel_Database().Id;

            // Limpar os contatos do db
            var contatos = _contatoRepository.BuscarTodos(usuarioId);

            foreach (var contato in contatos)
            {
                _contatoRepository.Apagar(contato);
            }

            // Remove o usuário criado no OrganizarPreTeste()
            _usuarioRepository.Apagar(_usuarioRepository.ListarPorId(usuarioId));
        }


    }

}

