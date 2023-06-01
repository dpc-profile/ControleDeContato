using System;

using ControleDeContatos.Controllers;
using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Moq;

using Xunit;

namespace ControleDeContatos.Tests.Tests.Controllers
{
    public class AlterarSenhaControllerTests
    {
        [Fact]
        public void TestarIndex()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockAlterarSenhaServices = new Mock<IAlterarSenhaServices>();
            var mockSessao = new Mock<ISessao>();

            //Faz o setup chamando o AlterarSenha
            mockAlterarSenhaServices.Setup(s => s.AlterarSenha(It.IsAny<AlterarSenhaModel>()));
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new AlterarSenhaController(
                mockSessao.Object,
                mockAlterarSenhaServices.Object){ TempData = tempData };

            // Act
            var result = controller.Index();

            // Assert
            // Confere se o index retornou normalmente
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestarAlterar_ValidModel()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockAlterarSenhaServices = new Mock<IAlterarSenhaServices>();
            var mockSessao = new Mock<ISessao>();

            //Faz o setup chamando o AlterarSenha
            mockAlterarSenhaServices.Setup(s => s.AlterarSenha(It.IsAny<AlterarSenhaModel>()));
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new AlterarSenhaController(
                mockSessao.Object,
                mockAlterarSenhaServices.Object){ TempData = tempData };

            // Act
            var result = controller.Alterar(fakeUsuario.ModeloAlterarSenhaUsuario());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de sucesso
            Assert.True(controller.TempData.Values.Contains("Senha atualizada com sucesso"));
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
            // Verifica se a model tem conteudo
            Assert.IsAssignableFrom<AlterarSenhaModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void TestarAlterar_InvalidModel()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockAlterarSenhaServices = new Mock<IAlterarSenhaServices>();
            var mockSessao = new Mock<ISessao>();

            mockAlterarSenhaServices.Setup(s => s.AlterarSenha(It.IsAny<AlterarSenhaModel>()));
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new AlterarSenhaController(
                mockSessao.Object,
                mockAlterarSenhaServices.Object){ TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            var result = controller.Alterar(fakeUsuario.ModeloAlterarSenhaUsuario());

            // Assert
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
            // Verifica se retornou o alterarSenhaUsuario
            Assert.NotNull(viewResult.Model);
        }

        [Fact]
        public void TestarAlterar_Exception()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockAlterarSenhaServices = new Mock<IAlterarSenhaServices>();
            var mockSessao = new Mock<ISessao>();

            //Faz o setup chamando o AlterarSenha
            mockAlterarSenhaServices.Setup(s => s.AlterarSenha(It.IsAny<AlterarSenhaModel>()))
                                    .Throws(new Exception());
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new AlterarSenhaController(
                mockSessao.Object,
                mockAlterarSenhaServices.Object){ TempData = tempData };

            // Assert
            var result = controller.Alterar(fakeUsuario.ModeloAlterarSenhaUsuario());

            // Assert
            // Verifica se a tempData deu MensagemErro
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro 
            Assert.Matches("Ops, não conseguimos atualizado o usuario", controller.TempData["MensagemErro"].ToString());
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
            // Verifica se retornou o alterarSenhaUsuario
            Assert.NotNull(viewResult.Model);

        }
    
    }
}