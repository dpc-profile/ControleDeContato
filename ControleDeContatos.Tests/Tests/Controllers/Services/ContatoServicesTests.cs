using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Services;

using Moq;

using Xunit;

namespace ControleDeContatos.Tests.Tests.Controllers.Services
{
    public class ContatoServicesTests
    {
        [Fact]
        public void TestarCriarContato()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.Adicionar(It.IsAny<ContatoModel>()));

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Act
            services.CriarContato(It.IsAny<ContatoModel>());
        }
        
        [Fact]
        public void TestarCriarContato_Exception()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.Adicionar(It.IsAny<ContatoModel>()))
                    .Throws(new Exception());

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Assert
            Assert.Throws<Exception>(
                // Act
                () => services.CriarContato(It.IsAny<ContatoModel>())
            );
        }

        [Fact]
        public void TestarAtualizarContato()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()))
                    .Returns(UmContato());

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Act
            services.AtualizarContato(UmContato());
        }

        [Fact]
        public void TestarAtualizarContato_Null()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()));

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Assert
            var mensagem = Assert.Throws<Exception>(
                // Act
                () => services.AtualizarContato(UmContato())
            );
            Assert.Equal("Contato não existe", mensagem.Message);
        }

        [Fact]
        public void TestarApagarContato()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()))
                    .Returns(UmContato());

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Act
            services.ApagarContato(It.IsAny<int>());
        }
        
        [Fact]
        public void TestarApagarContato_Null()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()));

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Assert
            var mensagem = Assert.Throws<Exception>(
                // Act
                () => services.ApagarContato(It.IsAny<int>())
            );
            Assert.Equal("Contato não existe", mensagem.Message);
        }

        [Fact]
        public void TestarBuscarContatos()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.BuscarTodos(It.IsAny<int>()))
                    .Returns(VariosContatos());

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Act
            var result = services.BuscarContatos(It.IsAny<int>());

            // Assert
            Assert.True(result.Count == 2);
        }
        [Fact]
        public void TestarBuscarContatos_Null()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.BuscarTodos(It.IsAny<int>()));

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Assert
            var mensagem = Assert.Throws<Exception>(
                // Act
                () => services.BuscarContatos(It.IsAny<int>())
            );
            Assert.Equal("Não existe contatos", mensagem.Message);
        }

        [Fact]
        public void TestarBuscarContato()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()))
                    .Returns(UmContato());

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Act
            var resposta = services.BuscarContato(It.IsAny<int>());

            // Assert
            Assert.IsType<ContatoModel>(resposta);
        }
        
        [Fact]
        public void TestarBuscarContato_Null()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()));

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Assert
            var mensagem = Assert.Throws<Exception>(
                // Act
                () => services.BuscarContato(It.IsAny<int>())
            );
            Assert.Equal("Contato não existe", mensagem.Message);
        }

        private List<ContatoModel> VariosContatos()
        {
            var contatos = new List<ContatoModel>();

            contatos.Add(new ContatoModel()
            {
                Id = 1,
                Nome = "Amilton Teste",
                Email = "amilton@teste.com",
                Celular = "11 98765-1234",
                UsuarioId = 1,
            });

            contatos.Add(new ContatoModel()
            {
                Id = 2,
                Nome = "Rodrigo Teste",
                Email = "rodrigo@teste.com",
                Celular = "11 98765-1234",
                UsuarioId = 1,
            });

            return contatos;
        }

        private ContatoModel UmContato()
        {
            ContatoModel contatos = new ContatoModel();

            contatos.Id = 3;
            contatos.Nome = "Arlindo Tester";
            contatos.Email = "arlindo@teste.com";
            contatos.Celular = "11 94325-1234";

            return contatos;
        }
    }
}