using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Services;
using ControleDeContatos.Services.Interfaces;
using ControleDeContatos.Tests.Tests.Controllers;

using Moq;

using Xunit;

namespace ControleDeContatos.Tests.Tests.Services
{
    public class LoginServicesTests
    {
        [Fact]
        public void TestarFazerLogin()
        {
            // Arrange
            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();

            mockRepo.Setup(s => s.BuscarPorLogin(It.IsAny<string>()))
                    .Returns(fakeUsuario.ModeloDadosUsuario());
            
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var services = new LoginServices(mockRepo.Object, mockEmail.Object);


            // Act
            services.FazerLogin(fakeUsuario.ModeloLoginValido(), mockSessao.Object);

        }
        [Fact]
        public void TestarFazerLogin_Exception_UsuarioInvalido()
        {
            // Arrange
            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();

            // Mock do usuarioRepository retornando nada
            mockRepo.Setup(s => s.BuscarPorLogin(It.IsAny<string>()));
            
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var services = new LoginServices(mockRepo.Object, mockEmail.Object);


            // Assert
            var mensagem = Assert.Throws<UsuarioInvalidoException>(
                // Act
                () => services.FazerLogin(fakeUsuario.ModeloLoginValido(), mockSessao.Object)
            );
            Assert.Equal("Usuário inválido", mensagem.Message);

        }
    }
}