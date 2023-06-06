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
        public void TestarNovoUsuario_View()
        {
            // Arrange
            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object);

            // Act
            var result = controller.NovoUsuario();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestarNovoUsuario_ValidModel()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();

            mockUsuarioServices.Setup(s => s.AdicionarUsuario(It.IsAny<UsuarioModel>()));

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            // Act
            var result = controller.NovoUsuario(It.IsAny<UsuarioModel>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de senha invalida
            Assert.True(controller.TempData.Values.Contains("Usuário cadastrado com sucesso"));
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se está redirecionando para a Index
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarNovoUsuario_InvalidModel()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };


            // Act
            controller.ModelState.AddModelError("fakeError", "fakeError");
            var result = controller.NovoUsuario(It.IsAny<UsuarioModel>());


            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void TestarNovoUsuario_Exception_LoginJaCadastrado()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();

            mockUsuarioServices.Setup(s => s.AdicionarUsuario(It.IsAny<UsuarioModel>()))
                               .Throws(new LoginJaCadastradoException("Login já cadastrado"));

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            // Act
            var result = controller.NovoUsuario(It.IsAny<UsuarioModel>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.Contains("Erro ao criar usuario: Login já cadastrado", controller.TempData["MensagemErro"].ToString());
            // Assert.Matches("Erro ao criar usuario: Login já cadastrado", controller.TempData["MensagemErro"].ToString()); 
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("NovoUsuario", redirectToActionResult.ActionName);
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

        [Fact]
        public void TestarEnviarLinkParaRedefinirSenha_ValidModel()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario());
            mockLoginServices.Setup(s => s.ValidaUsuarioCadastrado(It.IsAny<string>(), It.IsAny<string>()))
                             .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            var result = controller.EnviarLinkParaRedefinirSenha(fakeUsuario.ModeloRedefinirSenha());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de senha invalida
            Assert.True(controller.TempData.Values.Contains("Foi enviado para o email informado uma nova senha."));
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é a login
            Assert.Equal("Login", redirectToActionResult.ControllerName);
            // Verifica se está redirecionando para a Index
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        [Fact]
        public void TestarEnviarLinkParaRedefinirSenha_InvalidModel()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario());
            mockLoginServices.Setup(s => s.ValidaUsuarioCadastrado(It.IsAny<string>(), It.IsAny<string>()))
                             .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            controller.ModelState.AddModelError("fakeError", "fakeError");
            var result = controller.EnviarLinkParaRedefinirSenha(fakeUsuario.ModeloRedefinirSenha());

            // Assert
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void TestarEnviarLinkParaRedefinirSenha_Exception_EmailNaoEncontrado()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario());
            mockLoginServices.Setup(s => s.ValidaUsuarioCadastrado(It.IsAny<string>(), It.IsAny<string>()))
                             .Throws(new EmailNaoEncontradoException("O email informado não foi encontrado"));

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            var result = controller.EnviarLinkParaRedefinirSenha(fakeUsuario.ModeloRedefinirSenha());

            // Assert
            // Verifica se a tempData deu erro
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de email não encontrado
            Assert.True(controller.TempData.Values.Contains("O email informado não foi encontrado"));
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("RedefinirSenha", viewResult.ViewName);
        }
        [Fact]
        public void TestarEnviarLinkParaRedefinirSenha_Exception_LoginNaoEncontrado()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();

            mockSessao.Setup(s => s.BuscarSessaoUsuario());
            mockLoginServices.Setup(s => s.ValidaUsuarioCadastrado(It.IsAny<string>(), It.IsAny<string>()))
                             .Throws(new LoginNaoEncontradoException("O login informado não foi encontrado"));

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            var result = controller.EnviarLinkParaRedefinirSenha(fakeUsuario.ModeloRedefinirSenha());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de login não encontrado
            Assert.True(controller.TempData.Values.Contains("O login informado não foi encontrado"));
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("RedefinirSenha", viewResult.ViewName);
        }
        [Fact]
        public void TestarEnviarLinkParaRedefinirSenha_Exception_FalhaAoEnviarEmail()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();

            mockSessao.Setup(s => s.BuscarSessaoUsuario());
            // Mock do validaUsuarioCadastrado retornando um UsuarioModel
            mockLoginServices.Setup(s => s.ValidaUsuarioCadastrado(It.IsAny<string>(), It.IsAny<string>()))
                             .Returns(fakeUsuario.ModeloDadosUsuario());
            // Mock do EnviarNovaSenha estourando exceção FalhaAoEnviarEmailException
            mockLoginServices.Setup(s => s.EnviarNovaSenha(It.IsAny<string>(), It.IsAny<string>()))
                             .Throws(new FalhaAoEnviarEmailException("Não conseguimos enviar o email."));

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            var result = controller.EnviarLinkParaRedefinirSenha(fakeUsuario.ModeloRedefinirSenha());

            // Assert
            // Verifica se a tempData deu erro
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de falha ao enviar email
            Assert.Matches("Não conseguimos enviar o email.", controller.TempData["MensagemErro"].ToString());
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("RedefinirSenha", viewResult.ViewName);
        }
        [Fact]
        public void TestarEnviarLinkParaRedefinirSenha_Exception_Generico()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioServices> mockUsuarioServices = new Mock<IUsuarioServices>();
            Mock<ILoginServices> mockLoginServices = new Mock<ILoginServices>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();

            mockSessao.Setup(s => s.BuscarSessaoUsuario());
            // Mock do validaUsuarioCadastrado estourando Exception
            mockLoginServices.Setup(s => s.ValidaUsuarioCadastrado(It.IsAny<string>(), It.IsAny<string>()))
                             .Throws(new Exception());

            var controller = new LoginController(
                mockUsuarioServices.Object,
                mockSessao.Object,
                mockLoginServices.Object)
            { TempData = tempData };

            var result = controller.EnviarLinkParaRedefinirSenha(fakeUsuario.ModeloRedefinirSenha());

            // Assert
            // Verifica se a tempData deu erro
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de falha ao enviar email
            Assert.Matches("Ops, não foi possivel redefinir sua senha", controller.TempData["MensagemErro"].ToString());
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se está redirecionando para RedefinirSenha
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarSair()
        {
            // Arrange
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
                mockLoginServices.Object);

            // Act
            var result = controller.Sair();

            // Verifica se o retorno é RedirectToAction
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se foi redirecionado para o controller Login
            Assert.Equal("Login", redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}