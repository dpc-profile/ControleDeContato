using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Moq;
using ControleDeContatos.Controllers;
using Microsoft.AspNetCore.Mvc;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using ControleDeContatos.Services.Interfaces;

namespace ControleDeContatos.Tests.Tests.Controllers
{
    public class ContatoControllerTests
    {
        [Fact]
        public void TestaIndex()
        {
            // Arrange
            var mockRepo = new Mock<IContatoServices>();
            var mockSessao = new Mock<ISessao>();

            mockRepo.Setup(repo => repo.BuscarContatos(It.IsAny<int>()))
                    .Returns(fakeContato.VariosContatos());

            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            // Instanciar o controller usando o obj do mockRepo
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object);

            // Act
            // Faz a chamada do index
            var result = controller.Index();

            // Assert
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Confere se o viewResult é uma lista de ContatoModels
            var model = Assert.IsAssignableFrom<IEnumerable<ContatoModel>>(
                viewResult.ViewData.Model);
            // Comfere se foi retornado os 2 contatos
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void TestaCriar_ValidState()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            // Instancia o mock do repository, instanciar o controller com o obj mockRepo e o tempData
            var mockRepo = new Mock<IContatoServices>();
            // Mock da Sessao
            var mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            // com o controller, criar os contatos do fakeContato.UmContato()
            var result = controller.Criar(fakeContato.UmContato());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de sucesso
            Assert.True(controller.TempData.Values.Contains("Contato cadastrado com sucesso"));
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestaCriar_InvalidState()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            // Instancia o mock do repository
            var mockRepo = new Mock<IContatoServices>();
            // Mock da Sessao
            var mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            // Chama o Criar passando um obj com informações de um contato
            var result = controller.Criar(fakeContato.UmContato());

            // Assert
            // Verifica se tem algma mensagem no temData
            Assert.Empty(controller.TempData);
            // Verifica se é uma view
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a model tem conteudo
            Assert.IsAssignableFrom<ContatoModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void TestarCriar_View()
        {
            // Arrange
            var mockRepo = new Mock<IContatoServices>();
            var mockSessao = new Mock<ISessao>();

            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object);

            // Act
            var result = controller.Criar();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestarCriar_Exception()
        {
            // Arrange            
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockRepo = new Mock<IContatoServices>();
            var mockSessao = new Mock<ISessao>();
            //Faz o setup chamando o ApagarContato e estourando excessão
            mockRepo.Setup(repo => repo.CriarContato(It.IsAny<ContatoModel>()))
                    .Throws(new Exception());

            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());
            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };


            // Act
            // Faz a chamada do Apagar
            var result = controller.Criar(It.IsAny<ContatoModel>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.Matches("Ops, não conseguimos cadastrar o contato", controller.TempData["MensagemErro"].ToString()); 
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
            
        }

        [Fact]
        public void TestarEditar()
        {
            // Arrange
            // Cria o mock, e faz o setup chamando o BuscarContato e retornando um contato
            var mockRepo = new Mock<IContatoServices>();
            // Mock da Sessao
            var mockSessao = new Mock<ISessao>();
            mockRepo.Setup(repo => repo.BuscarContato(It.IsAny<int>()))
                    .Returns(fakeContato.UmContato());
            // Instanciar o controller usando o obj do mockRepo
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object);

            // Act
            // Faz a chamada do editar
            var result = controller.Editar(It.IsAny<int>());

            // Assert
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Confere se o model tem algo
            Assert.IsAssignableFrom<ContatoModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void TestarApagarConfirmacao()
        {
            // Arrange
            // Cria o mock, e faz o setup chamando o BuscarContato e retornando um contato
            var mockRepo = new Mock<IContatoServices>();
            // Mock da Sessao
            var mockSessao = new Mock<ISessao>();
            mockRepo.Setup(repo => repo.BuscarContato(It.IsAny<int>()))
                    .Returns(fakeContato.UmContato());
            // Instanciar o controller usando o obj do mockRepo
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object);

            // Act
            // Faz a chamada do ApagarConfirmacao passando o id de teste
            var result = controller.ApagarConfirmacao(It.IsAny<int>());

            // Assert
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Confere se o model tem algo
            Assert.IsAssignableFrom<ContatoModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void TestarApagar()
        {
            // Arrange            
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            // Cria o mock do repository e sessao
            var mockRepo = new Mock<IContatoServices>();
            var mockSessao = new Mock<ISessao>();
            //Faz o setup chamando o ApagarContato e retornando true
            mockRepo.Setup(repo => repo.ApagarContato(It.IsAny<int>()));

            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());
            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            // Faz a chamada do Apagar
            var result = controller.Apagar(It.IsAny<int>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de sucesso
            Assert.True(controller.TempData.Values.Contains("Contato apagado com sucesso"));
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
            // Cria o mock do repository e sessao
            var mockRepo = new Mock<IContatoServices>();
            var mockSessao = new Mock<ISessao>();
            //Faz o setup chamando o ApagarContato e estourando excessão
            mockRepo.Setup(repo => repo.ApagarContato(It.IsAny<int>()))
                    .Throws(new Exception());

            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());
            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };


            // Act
            // Faz a chamada do Apagar
            var result = controller.Apagar(It.IsAny<int>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.Matches("Ops, não conseguimos apagar o contato", controller.TempData["MensagemErro"].ToString()); 
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarAlterar_ValidModel()
        {
            // Arrange
            // Cria um contato
            ContatoModel contato = new ContatoModel()
            {
                Id = 3,
                Nome = "Arlindo Tester",
                Email = "arlindo@teste.com",
                Celular = "11 94325-1234"

            };
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            // Cria o mock do repository e sessao
            var mockRepo = new Mock<IContatoServices>();
            var mockSessao = new Mock<ISessao>();
            // Faz o setup chamando o Atualizar passando o contato e retornando o proprio contato
            mockRepo.Setup(repo => repo.AtualizarContato(It.IsAny<ContatoModel>()));
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());
            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            // Faz a chamada do Alterar passando o contato criado
            var result = controller.Alterar(contato);

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de senha invalida
            Assert.True(controller.TempData.Values.Contains("Contato atualizado com sucesso"));
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se foi redirecionado para algum lugar
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se está redirecionando para a Index
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarAlterar_InvalidModel()
        {
            // Cria o mock do repository e sessao
            var mockRepo = new Mock<IContatoServices>();
            var mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());

            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object);

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            // Faz a chamada do Alterar passando um contato vazio
            var result = controller.Alterar(new ContatoModel());

            // Assert
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica a view de redirect é a Editar
            Assert.Equal("Editar", viewResult.ViewName);
            // Confere se o model tem algo
            Assert.IsAssignableFrom<ContatoModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void TestarAlterar_Exception()
        {
            // Arrange            
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockRepo = new Mock<IContatoServices>();
            var mockSessao = new Mock<ISessao>();
            //Faz o setup chamando o ApagarContato e estourando excessão
            mockRepo.Setup(repo => repo.AtualizarContato(It.IsAny<ContatoModel>()))
                    .Throws(new Exception());

            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(fakeUsuario.ModeloDadosUsuario());
            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };


            // Act
            // Faz a chamada do Apagar
            var result = controller.Alterar(It.IsAny<ContatoModel>());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.Matches("Ops, não conseguimos atualizado o contato", controller.TempData["MensagemErro"].ToString()); 
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica se a controller é nula
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se foi redirecionado para Index do controller
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
       
        
    }
}