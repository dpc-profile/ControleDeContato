using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Moq;
using ControleDeContatos.Repository;
using ControleDeContatos.Controllers;
using Microsoft.AspNetCore.Mvc;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;

namespace ControleDeContatos.Tests.Tests
{
    public class ContatoControllerTests
    {

        [Fact]
        public void TestaIndex()
        {
            // Arrange
            // Instancia o mock do repository
            // Popula o mock com o CriarVariosContatos()
            // Instanciar o controller usando o obj do mockRepo
            var mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(repo => repo.BuscarTodos())
                .Returns(CriarVariosContatos());
            var controller = new ContatoController(mockRepo.Object);

            // Act
            // Faz a chamada do index
            var result = controller.Index();

            // Assert
            // Confere o se o tipo é viewResult
            // Confere se o viewResult é uma lista de ContatoModels
            // Comfere se a model está com os 2 contatos adicionados
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ContatoModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void TestaCriarValidStateSucesso()
        {
            // Arrange
            // Prepara e cria o tempData
            // Instancia o mock do repository
            // Instanciar o controller com o obj mockRepo e o tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockRepo = new Mock<IContatoRepository>();
            var controller = new ContatoController(mockRepo.Object) { TempData = tempData };

            // Act
            // com o controller, criar os contatos do CriarUmContato()
            var result = controller.Criar(CriarUmContato());

            // Assert
            // Verifica se a tempData deu sucesso
            // Verifica o retorno RedirectToAction
            // Verifica se foi redirecionado para Index
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        [Fact]
        public void TestaCriarNotValidState()
        {
            // Arrange
            // Cria o TempData
            // Cria o mockRepo e o controller
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockRepo = new Mock<IContatoRepository>();
            var controller = new ContatoController(mockRepo.Object) { TempData = tempData };

            // Act
            // Força o ModelState não ser valido
            // Chama o criar
            controller.ModelState.AddModelError("fakeError", "fakeError");
            var result = controller.Criar();

            // Assert
            // Confere se o tempData retornou vazio
            // Confere se retornou view
            // Confere se o state é invalido
            Assert.Empty(controller.TempData);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Invalid", viewResult.ViewData.ModelState.ValidationState.ToString());

        }

        [Fact]
        public void TestarEditar()
        {
            // Arrange
            var mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(repo => repo.ListarPorId(1))
                .Returns(CriarUmContato());
            var controller = new ContatoController(mockRepo.Object);

            // Act
            // Faz a chamada do index
            var result = controller.Editar(1);

            // Assert
            // Confere o se o tipo é viewResult
            // Confere se o viewResul é um ContatoModel
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ContatoModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void TestarApagarConfirmacao()
        {
            // Arrange
            // Instancia o mock do repository
            // Popula o mock com o CriarVariosContatos()
            // Instanciar o controller usando o obj do mockRepo
            var mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(repo => repo.ListarPorId(1))
                .Returns(CriarUmContato());
            var controller = new ContatoController(mockRepo.Object);

            // Act
            // Faz a chamada do index
            var result = controller.ApagarConfirmacao(1);

            // Assert
            // Confere o se o tipo é viewResult
            // Confere se o viewResul é um ContatoModel
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ContatoModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void TestarApagar()
        {
            // Arrange
            // Instancia o mock do repository
            // Popula o mock com o CriarVariosContatos()
            // Instanciar o controller usando o obj do mockRepo
            int testId = 3;
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(repo => repo.Apagar(testId))
                .Returns(true);
            var controller = new ContatoController(mockRepo.Object) { TempData = tempData };

            // Act
            // Faz a chamada do Apagar
            var result = controller.Apagar(testId);

            // Assert
            // Verifica se a tempData deu sucesso
            // Verifica o retorno RedirectToAction
            // Verifica se foi redirecionado para Index
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarAlterarValidModel()
        {
            ContatoModel contato = new ContatoModel()
            {
                Id = 3,
                Nome = "Arlindo Tester",
                Email = "arlindo@teste.com",
                Celular = "11 94325-1234"

            };
            // Arrange
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(repo => repo.Atualizar(contato))
                .Returns(contato);
            var controller = new ContatoController(mockRepo.Object) { TempData = tempData };

            // Act
            var result = controller.Alterar(contato);

            // Assert
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        private List<ContatoModel> CriarVariosContatos()
        {
            var contatos = new List<ContatoModel>();

            contatos.Add(new ContatoModel()
            {
                Id = 1,
                Nome = "Amilton Teste",
                Email = "amilton@teste.com",
                Celular = "11 98765-1234"
            });

            contatos.Add(new ContatoModel()
            {
                Id = 2,
                Nome = "Rodrigo Teste",
                Email = "rodrigo@teste.com",
                Celular = "11 98765-1234"
            });

            return contatos;
        }

        private ContatoModel CriarUmContato()
        {
            ContatoModel contatos = new ContatoModel();

            contatos.Id = 3;
            contatos.Nome = "Arlindo Tester";
            contatos.Email = "arlindo@teste.com";
            contatos.Celular = "11 94325-1234";

            return contatos;
        }
    }
}