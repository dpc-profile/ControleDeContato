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

namespace Services
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
    }
}