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
            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            var mockEmail = new Mock<IEmail>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(ModeloDadosUsuario());

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object);

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
            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object);

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
            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object);

            // Act
            var result = controller.RedefinirSenha();

            // Assert
            // Confere o se o tipo é viewResult
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestarEntrar_ValidModel_UsuarioESenhaValidos()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();
            // Faz setup chamando o BuscarPorLogin passando o LoginModel.login e retorna o UsuarioModel
            mockRepo.Setup(s => s.BuscarPorLogin(It.IsAny<string>()))
                    .Returns(ModeloDadosUsuario());

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object) { TempData = tempData };

            // Act
            var result = controller.Entrar(ModeloLoginValido());

            // Verifica se o retorno é RedirectToAction
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se foi redirecionado para o controller Home
            Assert.Equal("Home", redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarEntrar_ValidModel_UsuarioValidoSenhaInvalida()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();
            // Faz setup chamando o BuscarPorLogin passando o LoginModel.login e retorna o UsuarioModelSenhaInvalida
            mockRepo.Setup(s => s.BuscarPorLogin(It.IsAny<string>()))
                    .Returns(ModeloDadosUsuario_SenhaInvalida());

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object) { TempData = tempData };

            // Act
            var result = controller.Entrar(ModeloLoginValido());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de senha invalida
            Assert.True(controller.TempData.Values.Contains("Senha inválida"));
            // Confere o se o tipo é viewResult
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void TestarEntrar_ValidModel_UsuarioInvalido()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();
            // Faz setup chamando o BuscarPorLogin passando o LoginModel.login e retornando nada
            mockRepo.Setup(s => s.BuscarPorLogin(It.IsAny<string>()));

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object) { TempData = tempData };


            // Act
            var result = controller.Entrar(ModeloLoginValido());


            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de Usuário inválido
            Assert.True(controller.TempData.Values.Contains("Usuário inválido"));
            // Confere o se o tipo é viewResult
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void TestarEntrar_InvalidModel()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();
            // Faz setup chamando o BuscarPorLogin passando o LoginModel.login e retornando nada
            mockRepo.Setup(s => s.BuscarPorLogin(It.IsAny<string>()));

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object) { TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            var result = controller.Entrar(ModeloLoginValido());

            // Assert
            // Confere o se o tipo é viewResult
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void TestarEnviarLinkParaRedefinirSenha_ValidModel_UsuarioValido_EmailEnviado()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();

            // Faz o setup chamando o BuscarPorEmailELogin, passando o email e login do RedefinirSenhaModel, retornando o UsuarioModel
            mockRepo.Setup(s =>
                            s.BuscarPorEmailELogin(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(ModeloDadosUsuario());
            // Faz o setup chamando o Enviar, passando o email do UsuarioModel, assunto e a mensagem do email, retornando false
            mockEmail.Setup(s => s.Enviar(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(true);

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object) { TempData = tempData };

            // Act
            var result = controller.EnviarLinkParaRedefinirSenha(ModeloRedefinirSenha());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de Usuário inválido
            Assert.True(controller.TempData.Values.Contains("Foi enviado para o email cadastrado uma nova senha."));
            // Verifica se o retorno é RedirectToAction
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se foi redirecionado para o controller Home
            Assert.Equal("Login", redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarEnviarLinkParaRedefinirSenha_ValidModel_UsuarioValido_EmailNaoEnviado()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();

            // Faz o setup chamando o BuscarPorEmailELogin, passando o email e login do RedefinirSenhaModel, retornando o UsuarioModel
            mockRepo.Setup(s =>
                            s.BuscarPorEmailELogin(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(ModeloDadosUsuario());
            // Faz o setup chamando o Enviar, passando o email do UsuarioModel, assunto e a mensagem do email, retornando false
            mockEmail.Setup(s => s.Enviar(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(false);

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object) { TempData = tempData };

            // Act
            var result = controller.EnviarLinkParaRedefinirSenha(ModeloRedefinirSenha());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de Usuário inválido
            Assert.True(controller.TempData.Values.Contains("Não conseguimos enviar o email, a senha não foi resetada, tente novamente"));
            // Verifica se o retorno é RedirectToAction
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se foi redirecionado para o controller Home
            Assert.Equal("Login", redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarEnviarLinkParaRedefinirSenha_ValidModel_UsuarioInvalido()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object) { TempData = tempData };

            // Act
            var result = controller.EnviarLinkParaRedefinirSenha(ModeloRedefinirSenha());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de Usuário inválido
            Assert.True(controller.TempData.Values.Contains("Não foi possivel redefinir sua senha, verifique os dados informados"));
            // Confere o se o tipo é viewResult
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void TestarEnviarLinkParaRedefinirSenha_InvalidModel()
        {
            // Arrange
            DefaultHttpContext httpContext = new DefaultHttpContext();
            TempDataDictionary tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object) { TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            var result = controller.EnviarLinkParaRedefinirSenha(ModeloRedefinirSenha());

            // Assert
            // Confere o se o tipo é viewResult
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void TestarSair()
        {
            // Arrange
            Mock<IUsuarioRepository> mockRepo = new Mock<IUsuarioRepository>();
            Mock<ISessao> mockSessao = new Mock<ISessao>();
            Mock<IEmail> mockEmail = new Mock<IEmail>();

            var controller = new LoginController(mockRepo.Object, mockSessao.Object, mockEmail.Object);

            // Act
            var result = controller.Sair();

            // Verifica se o retorno é RedirectToAction
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se foi redirecionado para o controller Login
            Assert.Equal("Login", redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }


        public UsuarioModel ModeloDadosUsuario()
        {
            // Senha: teste
            var usuarioModel = new UsuarioModel()
            {
                Id = 1,
                Nome = "Padronos Tester",
                Login = "padronos",
                Email = "padronos@gmail.com",
                Senha = "2e6f9b0d5885b6010f9167787445617f553a735f",
                Perfil = Enums.PerfilEnums.Padrao,
                DataCadastro = DateTime.Now
            };

            return usuarioModel;
        }
        public UsuarioModel ModeloDadosUsuario_SenhaInvalida()
        {
            // A senha não está convertida em hash
            var usuarioModel = new UsuarioModel()
            {
                Id = 1,
                Nome = "Padronos Tester",
                Login = "padronos",
                Email = "padronos@gmail.com",
                Senha = "teste",
                Perfil = Enums.PerfilEnums.Padrao,
                DataCadastro = DateTime.Now
            };

            return usuarioModel;
        }

        public LoginModel ModeloLoginValido()
        {
            return new LoginModel()
            {
                Login = "padronos",
                Senha = "teste"
            };
        }

        public RedefinirSenhaModel ModeloRedefinirSenha()
        {
            return new RedefinirSenhaModel()
            {
                Login = "padronos",
                Email = "padronos@gmail.com"
            };
        }
    }
}