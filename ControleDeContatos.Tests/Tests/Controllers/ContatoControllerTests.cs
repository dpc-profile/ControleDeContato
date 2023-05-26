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
using ControleDeContatos.Helper;

namespace ControleDeContatos.Tests.Tests.Controllers
{
    public class ContatoControllerTests
    {

        [Fact]
        public void TestaIndex()
        {
            // Arrange
            // Cria o mock, e faz o setup chamando o Apagar e retornando false
            var mockRepo = new Mock<IContatoRepository>();
            var mockSessao = new Mock<ISessao>();

            mockRepo.Setup(repo => repo.BuscarTodos(It.IsAny<int>()))
                    .Returns(CriarVariosContatos());

            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(ModeloDadosUsuario());

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
            var mockRepo = new Mock<IContatoRepository>();
            // Mock da Sessao
            var mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(ModeloDadosUsuario());

            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            // com o controller, criar os contatos do CriarUmContato()
            var result = controller.Criar(CriarUmContato());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemSucesso"));
            // Verifica se a mensagem é de sucesso
            Assert.True(controller.TempData.Values.Contains("Contato cadastrado com sucesso"));
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
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
            var mockRepo = new Mock<IContatoRepository>();
            // Mock da Sessao
            var mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(ModeloDadosUsuario());

            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            // Chama o Criar passando um obj com informações de um contato
            var result = controller.Criar(CriarUmContato());

            // Assert
            // Verifica se tem algma mensagem no temData
            Assert.Empty(controller.TempData);
            // Verifica se é uma view
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a model tem conteudo
            Assert.IsAssignableFrom<ContatoModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void TestarEditar()
        {
            // Arrange
            // Cria o mock, e faz o setup chamando o ListarPorId e retornando um contato
            var mockRepo = new Mock<IContatoRepository>();
            // Mock da Sessao
            var mockSessao = new Mock<ISessao>();
            mockRepo.Setup(repo => repo.ListarPorId(It.IsAny<int>()))
                    .Returns(CriarUmContato());
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
            // Cria o mock, e faz o setup chamando o ListarPorId e retornando um contato
            var mockRepo = new Mock<IContatoRepository>();
            // Mock da Sessao
            var mockSessao = new Mock<ISessao>();
            mockRepo.Setup(repo => repo.ListarPorId(It.IsAny<int>()))
                    .Returns(CriarUmContato());
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
        public void TestarApagar_ApagarTrue()
        {
            // Arrange            
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            // Cria o mock do repository e sessao
            var mockRepo = new Mock<IContatoRepository>();
            var mockSessao = new Mock<ISessao>();
            //Faz o setup chamando o Apagar e retornando true
            mockRepo.Setup(repo => repo.Apagar(It.IsAny<int>()))
                    .Returns(true);
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(ModeloDadosUsuario());
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
        public void TestarApagar_ApagarFalse()
        {
            // Arrange
            // Cria o tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            // Cria o mock do repository e sessao
            var mockRepo = new Mock<IContatoRepository>();
            var mockSessao = new Mock<ISessao>();
            //Faz o setup chamando o Apagar e retornando true
            mockRepo.Setup(repo => repo.Apagar(It.IsAny<int>()))
                    .Returns(false);
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(ModeloDadosUsuario());

            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            // Faz a chamada do Apagar
            var result = controller.Apagar(It.IsAny<int>());

            // Assert
            // Verifica se a tempData deu erro
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de erro
            Assert.True(controller.TempData.Values.Contains("Ops, não conseguimos apagar o contato"));
            // Verifica se o retorno é RedirectToAction
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            // Verifica o redirect tem algo
            Assert.Null(redirectToActionResult.ControllerName);
            // Verifica se está redirecionando para a Index
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestarAlterar_ValidModel_AtualizarNotNull()
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
            var mockRepo = new Mock<IContatoRepository>();
            var mockSessao = new Mock<ISessao>();
            // Faz o setup chamando o Atualizar passando o contato e retornando o proprio contato
            mockRepo.Setup(repo => repo.Atualizar(It.IsAny<ContatoModel>()))
                    .Returns(contato);
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(ModeloDadosUsuario());
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
        public void TestarAlterar_ValidModel_AtualizarNull()
        {
            // Arrange
            // Cria a variavel tempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            // Cria o mock do repository e sessao
            var mockRepo = new Mock<IContatoRepository>();
            var mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(ModeloDadosUsuario());
            // Cria o controller, com o mock do repository, da sessão e o tempData
            var controller = new ContatoController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            // Faz a chamada do Alterar passando um contato vazio
            var result = controller.Alterar(new ContatoModel());

            // Assert
            // Verifica se a tempData deu sucesso
            Assert.True(controller.TempData.ContainsKey("MensagemErro"));
            // Verifica se a mensagem é de senha invalida
            Assert.True(controller.TempData.Values.Contains("Ops, não conseguimos atualizado o contato, tente novamente."));
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
            var mockRepo = new Mock<IContatoRepository>();
            var mockSessao = new Mock<ISessao>();
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(repo => repo.BuscarSessaoUsuario())
                      .Returns(ModeloDadosUsuario());

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


        public UsuarioModel ModeloDadosUsuario()
        {
            var usuarioModel = new UsuarioModel()
            {
                Id = 1,
                Nome = "Padronos Tester",
                Login = "padronos",
                Email = "padronos@gmail.com",
                Senha = "teste",
                Perfil = Enums.PerfilEnums.Padrao,
                DataCadastro = DateTime.Now,
                Contatos = CriarVariosContatos(),

            };

            return usuarioModel;
        }
        private List<ContatoModel> CriarVariosContatos()
        {
            var contatos = new List<ContatoModel>();

            contatos.Add(new ContatoModel()
            {
                Id = 1,
                Nome = "Amilton Teste",
                Email = "amilton@teste.com",
                Celular = "11 98765-1234",
                UsuarioId = 1,
            });

            contatos.Add(new ContatoModel()
            {
                Id = 2,
                Nome = "Rodrigo Teste",
                Email = "rodrigo@teste.com",
                Celular = "11 98765-1234",
                UsuarioId = 1,
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