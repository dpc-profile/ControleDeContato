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
                    .Returns(fakeContato.UmContato());

            ContatoServices services = new ContatoServices(mockRepo.Object);

            // Act
            services.AtualizarContato(fakeContato.UmContato());
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
                () => services.AtualizarContato(fakeContato.UmContato())
            );
            Assert.Equal("Contato não existe", mensagem.Message);
        }

        [Fact]
        public void TestarApagarContato()
        {
            // Arrange
            Mock<IContatoRepository> mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()))
                    .Returns(fakeContato.UmContato());

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
                    .Returns(fakeContato.VariosContatos());

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
                    .Returns(fakeContato.UmContato());

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

    }
}