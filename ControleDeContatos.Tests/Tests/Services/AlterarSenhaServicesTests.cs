using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Services;
using ControleDeContatos.Services.Interfaces;
using ControleDeContatos.Tests.Tests.Controllers;

using Moq;

using namesource.ControleDeContatos.Exceptions;

using Xunit;

namespace ControleDeContatos.Tests.Tests.Services
{
    public class AlterarSenhaServicesTests
    {
        [Fact]
        public void TestarAlterarSenha()
        {
            // Arrange
            var mockUsuarioServices = new Mock<IUsuarioServices>();

            mockUsuarioServices.Setup(s => s.BuscarUsuario(It.IsAny<int>()))
                               .Returns(fakeUsuario.ModeloDadosUsuario());

            var services = new AlterarSenhaServices(mockUsuarioServices.Object);

            // Act
            services.AlterarSenha(fakeUsuario.ModeloAlterarSenhaUsuario());

        }
        [Fact]
        public void TestarAlterarSenha_Exception_UsuarioInvalido()
        {
            // Arrange
            var mockUsuarioServices = new Mock<IUsuarioServices>();

            // Mock do UsuarioServices retornando nada
            mockUsuarioServices.Setup(s => s.BuscarUsuario(It.IsAny<int>()));

            var services = new AlterarSenhaServices(mockUsuarioServices.Object);

            // Assert
            var message = Assert.Throws<UsuarioInvalidoException>(
                // Act
                () => services.AlterarSenha(fakeUsuario.ModeloAlterarSenhaUsuario())
            );

            Assert.Equal("Usuário não encontrado!", message.Message);
        }
        [Fact]
        public void TestarAlterarSenha_Exception_SenhaNaoConfere()
        {
            // Arrange
            var mockUsuarioServices = new Mock<IUsuarioServices>();

            mockUsuarioServices.Setup(s => s.BuscarUsuario(It.IsAny<int>()))
                               .Returns(fakeUsuario.ModeloDadosUsuario());

            var services = new AlterarSenhaServices(mockUsuarioServices.Object);

            // Assert
            var message = Assert.Throws<SenhaNaoConfereException>(
                // Act
                () => services.AlterarSenha(fakeUsuario.ModeloInvalidoAlterarSenhaUsuario_SenhaNaoConfere())
            );

            Assert.Equal("Senha atual não confere!", message.Message);

        }
        [Fact]
        public void TestarAlterarSenha_Exception_NovaSenhaIgualAtual()
        {
            // Arrange
            var mockUsuarioServices = new Mock<IUsuarioServices>();

            mockUsuarioServices.Setup(s => s.BuscarUsuario(It.IsAny<int>()))
                               .Returns(fakeUsuario.ModeloDadosUsuario());

            var services = new AlterarSenhaServices(mockUsuarioServices.Object);

            // Assert
            var message = Assert.Throws<NovaSenhaIgualAtualException>(
                // Act
                () => services.AlterarSenha(fakeUsuario.ModeloInvalidoAlterarSenhaUsuario_SenhaIguais())
            );

            Assert.Equal("Nova senha deve ser diferente da senha atual!", message.Message);

        }
    }
}