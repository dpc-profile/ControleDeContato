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
            var mockRepo = new Mock<IUsuarioRepository>();
            var mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            // Instanciar o controller usando o obj do mockRepo
            var controller = new AlterarSenhaController(mockRepo.Object, mockSessao.Object);

            // Act
            var result = controller.Index();

            // Assert
            // Confere se o index retornou normalmente
            Assert.NotNull(result);
        }

        [Fact]
        public void TestarAlterar_ValidModel()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            
            var mockRepo = new Mock<IUsuarioRepository>();
            var mockSessao = new Mock<ISessao>();

            //Faz o setup chamando o AlterarSenha
            mockRepo.Setup(r => r.AlterarSenha(fakeUsuario.alterarSenhaUsuario()));
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new AlterarSenhaController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            var result = controller.Alterar(fakeUsuario.alterarSenhaUsuario());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
            // Verifica se retornou o alterarSenhaUsuario
            Assert.NotNull(viewResult.Model);
        }
        
        [Fact]
        public void TestarAlterar_InvalidModel()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockRepo = new Mock<IUsuarioRepository>();
            var mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            var controller = new AlterarSenhaController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            var result = controller.Alterar(fakeUsuario.alterarSenhaUsuario());

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
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockRepo = new Mock<IUsuarioRepository>();
            var mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Throws(new Exception());
                      
            var controller = new AlterarSenhaController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Assert
            var result = controller.Alterar(fakeUsuario.alterarSenhaUsuario());

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