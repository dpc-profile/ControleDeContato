using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Repository;
using ControleDeContatos.Services;
using ControleDeContatos.Tests.Tests.Controllers;

using Moq;

using namesource.ControleDeContatos.Exceptions;

using Xunit;

namespace ControleDeContatos.Tests.Tests.Services
{
    public class UsuarioServicesTests
    {
        [Fact]
        public void TestarAdicionarUsuario()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.BuscarPorLogin(It.IsAny<string>()));
            mockRepo.Setup(s => s.BuscarPorEmail(It.IsAny<string>()));

            var services = new UsuarioServices(mockRepo.Object);

            // Act
            services.AdicionarUsuario(fakeUsuario.ModeloDadosUsuario());
        }

        [Fact]
        public void TestarAdicionarUsuario_Exception_LoginJaCadastrado()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.BuscarPorLogin(It.IsAny<string>()))
                    .Returns(fakeUsuario.ModeloDadosUsuario);
            mockRepo.Setup(s => s.BuscarPorEmail(It.IsAny<string>()));

            var services = new UsuarioServices(mockRepo.Object);


            // Assert
            var message = Assert.Throws<LoginJaCadastradoException>(
                // Act
                () => services.AdicionarUsuario(fakeUsuario.ModeloDadosUsuario())
            );

            Assert.Equal("Login já cadastrado", message.Message);
        }

        [Fact]
        public void TestarAdicionarUsuario_Exception_EmailJaCadastrado()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.BuscarPorLogin(It.IsAny<string>()));
            mockRepo.Setup(s => s.BuscarPorEmail(It.IsAny<string>()))
                    .Returns(fakeUsuario.ModeloDadosUsuario); ;

            var services = new UsuarioServices(mockRepo.Object);


            // Assert
            var message = Assert.Throws<EmailJaCadastradoException>(
                // Act
                () => services.AdicionarUsuario(fakeUsuario.ModeloDadosUsuario())
            );

            Assert.Equal("Email já cadastrado", message.Message);
        }

        [Fact]
        public void TestarApagarUsuario()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()))
                    .Returns(fakeUsuario.ModeloDadosUsuario());

            var services = new UsuarioServices(mockRepo.Object);

            // Act
            services.ApagarUsuario(It.IsAny<int>());
        }

        [Fact]
        public void TestarApagarUsuario_Exception_UsuarioNaoExiste()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()));

            var services = new UsuarioServices(mockRepo.Object);

            // Assert
            var message = Assert.Throws<Exception>(
                // Act
                () => services.ApagarUsuario(It.IsAny<int>())
            );

            Assert.Equal("Usuário não existe", message.Message);
        }

        [Fact]
        public void TestarAtualizarUsuario()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()))
                    .Returns(fakeUsuario.ModeloDadosUsuario());

            var services = new UsuarioServices(mockRepo.Object);

            // Act
            services.AtualizarUsuario(fakeUsuario.ModeloUsuarioSemSenha());
        }

        [Fact]
        public void TestarAdicionarUsuario_Exception_UsuarioNaoExiste()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()));

            var services = new UsuarioServices(mockRepo.Object);

            // Assert
            var message = Assert.Throws<Exception>(
                // Act
                () => services.AtualizarUsuario(fakeUsuario.ModeloUsuarioSemSenha())
            );

            Assert.Equal("Usuário não existe", message.Message);
        }
        [Fact]
        public void TestarAtualizarUsuarioComSenha()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()))
                    .Returns(fakeUsuario.ModeloDadosUsuario());

            var services = new UsuarioServices(mockRepo.Object);

            // Act
            services.AtualizarUsuarioComSenha(fakeUsuario.ModeloDadosUsuario());
        }
        [Fact]
        public void TestarAtualizarUsuarioComSenha_Exception_UsuarioInvalido()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()));

            var services = new UsuarioServices(mockRepo.Object);


            // Assert
            var message = Assert.Throws<UsuarioInvalidoException>(
                // Act
                () => services.AtualizarUsuarioComSenha(fakeUsuario.ModeloDadosUsuario())
            );

            Assert.Equal("Usuário não existe", message.Message);
        }
        [Fact]
        public void TestarBuscarUsuario()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.ListarPorId(It.IsAny<int>()));

            var services = new UsuarioServices(mockRepo.Object);

            //Act
            services.BuscarUsuario(It.IsAny<int>());
        }
        [Fact]
        public void TestarBuscarUsuarios()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.BuscarTodos());

            var services = new UsuarioServices(mockRepo.Object);

            //Act
            services.BuscarUsuarios();
        }
    }
}