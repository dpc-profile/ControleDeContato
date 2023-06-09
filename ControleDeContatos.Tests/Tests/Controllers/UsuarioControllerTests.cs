using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Controllers;
using ControleDeContatos.Models;
using ControleDeContatos.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Moq;

using namesource.ControleDeContatos.Exceptions;

using Xunit;

namespace ControleDeContatos.Tests.Tests.Controllers
{
    public class UsuarioControllerTests
    {
        [Fact]
        public void TestarIndex()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioServices>();

            mockRepo.Setup(repo => repo.BuscarUsuarios())
                    .Returns(fakeUsuario.VariosUsuarios());

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object);

            // Act
            // Faz a chamada do index
            var result = controller.Index();

            // Assert
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Confere se o viewResult é uma lista de ContatoModels
            var model = Assert
                .IsAssignableFrom<IEnumerable<UsuarioModel>>(viewResult.ViewData.Model);
            // Comfere se foi retornado os 2 contatos
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void TestarEditar()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioServices>();
            var mockSessao = new Mock<ISessao>();

            mockRepo.Setup(repo => repo.BuscarUsuario(It.IsAny<int>()))
                    .Returns(fakeUsuario.ModeloDadosUsuario());

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object);

            // Act
            // Faz a chamada do editar
            var result = controller.Editar(It.IsAny<int>());

            // Assert
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Confere se o model tem algo
            Assert.IsAssignableFrom<UsuarioModel>(viewResult.ViewData.Model);
        }
        
        [Fact]
        public void TestarApagarConfirmacao()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioServices>();

            mockRepo.Setup(repo => repo.BuscarUsuario(It.IsAny<int>()))
                    .Returns(fakeUsuario.ModeloDadosUsuario());

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object);

            // Act
            // Faz a chamada do editar
            var result = controller.ApagarConfirmacao(It.IsAny<int>());

            // Assert
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Confere se o model tem algo
            Assert.IsAssignableFrom<UsuarioModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void TestarApagar()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>()); 

            var mockRepo = new Mock<IUsuarioServices>();
            mockRepo.Setup(repo => repo.ApagarUsuario(It.IsAny<int>()));

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object){ TempData = tempData };
            // Cria o controller, com o mock do repository, da sessão e o tempData

            // Act
            // Faz a chamada do Apagar
            var result = controller.Apagar(It.IsAny<int>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de sucesso
            Assert.True(controller.TempData.Values.Contains("Usuário apagado com sucesso"));
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se está redirecionando para a Index
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        
        [Fact]
        public void TestarApagar_Exception()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>()); 

            var mockRepo = new Mock<IUsuarioServices>();
            mockRepo.Setup(repo => repo.ApagarUsuario(It.IsAny<int>()))
                    .Throws(new Exception());

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object){ TempData = tempData };
            // Cria o controller, com o mock do repository, da sessão e o tempData

            // Act
            // Faz a chamada do Apagar
            var result = controller.Apagar(It.IsAny<int>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.Matches("Ops, não conseguimos apagar o usuário", controller.TempData["MensagemErro"].ToString()); 
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarCriar_View()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioServices>();

            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new UsuarioController(mockRepo.Object);

            // Act
            var result = controller.Criar();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestarCriar_ValidState()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>()); 

            var mockRepo = new Mock<IUsuarioServices>();
            mockRepo.Setup(repo => repo.AdicionarUsuario(It.IsAny<UsuarioModel>()));

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object){ TempData = tempData };

            // Act
            // Faz a chamada do Apagar
            var result = controller.Criar(It.IsAny<UsuarioModel>());


            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de sucesso
            Assert.True(controller.TempData.Values.Contains("Usuário cadastrado com sucesso"));
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarCriar_InvalidState()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioServices>();
            mockRepo.Setup(repo => repo.AdicionarUsuario(It.IsAny<UsuarioModel>()));

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object);

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            // Faz a chamada do Apagar
            var result = controller.Criar(It.IsAny<UsuarioModel>());


            // Assert
            // Verifica se é uma view
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void TestarCriar_Exception_LoginJaCadastrado()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>()); 

            var mockRepo = new Mock<IUsuarioServices>();
            mockRepo.Setup(repo => repo.AdicionarUsuario(It.IsAny<UsuarioModel>()))
                    .Throws(new LoginJaCadastradoException("Login já cadastrado"));

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object){ TempData = tempData };

            // Act
            // Faz a chamada do Apagar
            var result = controller.Criar(It.IsAny<UsuarioModel>());


            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.Contains("Erro ao criar usuario: Login já cadastrado", controller.TempData["MensagemErro"].ToString());
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Criar", redirectToActionResult.ActionName);
            
        }
        
        [Fact]
        public void TestarCriar_Exception_EmailJaCadastrado()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>()); 

            var mockRepo = new Mock<IUsuarioServices>();
            mockRepo.Setup(repo => repo.AdicionarUsuario(It.IsAny<UsuarioModel>()))
                    .Throws(new EmailJaCadastradoException("Email já cadastrado"));

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object){ TempData = tempData };

            // Act
            // Faz a chamada do Apagar
            var result = controller.Criar(It.IsAny<UsuarioModel>());


            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.Contains("Erro ao criar usuario: Email já cadastrado", controller.TempData["MensagemErro"].ToString());
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Criar", redirectToActionResult.ActionName);
            
        }

        [Fact]
        public void TestarCriar_Exception_Generica()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>()); 

            var mockRepo = new Mock<IUsuarioServices>();
            mockRepo.Setup(repo => repo.AdicionarUsuario(It.IsAny<UsuarioModel>()))
                    .Throws(new Exception());

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object){ TempData = tempData };

            // Act
            // Faz a chamada do Apagar
            var result = controller.Criar(It.IsAny<UsuarioModel>());


            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.Matches("Ops, não conseguimos cadastrar o usuario", controller.TempData["MensagemErro"].ToString()); 
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarAlterar_ValidState()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>()); 

            var mockRepo = new Mock<IUsuarioServices>();
            mockRepo.Setup(repo => repo.AtualizarUsuario(It.IsAny<UsuarioSemSenhaModel>()));

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object){ TempData = tempData };

            // Act
            // Faz a chamada do Apagar
            var result = controller.Alterar(It.IsAny<UsuarioSemSenhaModel>());


            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de sucesso
            Assert.True(controller.TempData.Values.Contains("Usuário atualizado com sucesso"));
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        
        [Fact]
        public void TestarAlterar_InvalidState()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>()); 

            var mockRepo = new Mock<IUsuarioServices>();

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object){ TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            // Faz a chamada do Apagar
            var result = controller.Alterar(It.IsAny<UsuarioSemSenhaModel>());


            // Assert
             // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica a view de redirect é a Editar
            Assert.Equal("Editar", viewResult.ViewName);
        }

        [Fact]
        public void TestarAlterar_Exception()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>()); 

            var mockRepo = new Mock<IUsuarioServices>();
            mockRepo.Setup(repo => repo.AtualizarUsuario(It.IsAny<UsuarioSemSenhaModel>()))
                    .Throws(new Exception());

            // Instanciar o controller usando o obj do mockRepo
            var controller = new UsuarioController(mockRepo.Object){ TempData = tempData };

            // Act
            // Faz a chamada do Apagar
            var result = controller.Alterar(It.IsAny<UsuarioSemSenhaModel>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.Matches("Ops, não conseguimos atualizado o usuario", controller.TempData["MensagemErro"].ToString()); 
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}