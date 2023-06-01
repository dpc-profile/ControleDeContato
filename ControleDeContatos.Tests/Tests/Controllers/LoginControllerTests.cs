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
    public class LoginControllerTests
    {
        [Fact]
        public void TestarIndex_SessaoNotNull()
        {
            // Arrange
            var mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            var mockLoginServices = new Mock<ILoginServices>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object);

            // Act
            var result = controller.Index();

            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se foi redirecionado para o controller Home
            Assert.Equal("Home", redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarIndex_SessaoNull()
        {
            // Arrange
            var mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            var mockLoginServices = new Mock<ILoginServices>();

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object);

            // Act
            var result = controller.Index();

            // Assert
            // Confere o se o tipo é viewResult
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestarRedefinirSenha()
        {
            // Arrange
            var mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            var mockLoginServices = new Mock<ILoginServices>();

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object);

            // Act
            var result = controller.RedefinirSenha();

            // Assert
            // Confere o se o tipo é viewResult
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestarEntrar_ValidModel()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            var mockLoginServices = new Mock<ILoginServices>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            // Act
            var result = controller.Entrar(fakeUsuario.ModeloLoginValido());

            // Verifica se o retorno é RedirectToAction
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se foi redirecionado para o controller Home
            Assert.Equal("Home", redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarEntrar_InvalidModel()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            var result = controller.Entrar(It.IsAny<LoginModel>());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void TestarEntrar_Exception_UsuarioInvalido()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario());
            mockLoginServices.Setup(s => s.FazerLogin(It.IsAny<LoginModel>(), It.IsAny<ISessao>()))
                             .Throws(new UsuarioInvalidoException("Usuário inválido"));

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            var result = controller.Entrar(It.IsAny<LoginModel>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.True(controller.TempData.Values.Contains("Usuário inválido"));
            // Assert.Matches("Ops, não conseguimos apagar o contato", controller.TempData["MensagemErro"].ToString()); 
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarEntrar_Exception_SenhaInvalido()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario());
            mockLoginServices.Setup(s => s.FazerLogin(It.IsAny<LoginModel>(), It.IsAny<ISessao>()))
                             .Throws(new SenhaInvalidaException("Senha inválida"));

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            var result = controller.Entrar(It.IsAny<LoginModel>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.True(controller.TempData.Values.Contains("Senha inválida"));
            // Assert.Matches("Ops, não conseguimos apagar o contato", controller.TempData["MensagemErro"].ToString()); 
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarEntrar_Exception_Generico()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario());
            mockLoginServices.Setup(s => s.FazerLogin(It.IsAny<LoginModel>(), It.IsAny<ISessao>()))
                             .Throws(new Exception());

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            var result = controller.Entrar(It.IsAny<LoginModel>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.Matches("Ops, não foi possivel fazer o login", controller.TempData["MensagemErro"].ToString()); 
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        

    }
}