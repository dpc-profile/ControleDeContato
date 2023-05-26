using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Controllers;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repository;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Moq;

using Xunit;

namespace ControleDeContatos.Tests.Tests
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
                      .Returns(ModeloDadosUsuario());

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
            mockRepo.Setup(r => r.AlterarSenha(alterarSenhaUsuario()));
            // Faz setup buscando uma sessão e retornando o usuarioModel
            mockSessao.Setup(s => s.BuscarSessaoUsuario())
                      .Returns(ModeloDadosUsuario());

            var controller = new AlterarSenhaController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            var result = controller.Alterar(alterarSenhaUsuario());

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
                      .Returns(ModeloDadosUsuario());

            var controller = new AlterarSenhaController(mockRepo.Object, mockSessao.Object) { TempData = tempData };

            // Act
            // Adiciona um model com um erro falso
            controller.ModelState.AddModelError("fakeError", "fakeError");
            var result = controller.Alterar(alterarSenhaUsuario());

            // Assert
            // Confere o se o tipo é viewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Verifica se a view é para Index
            Assert.Equal("Index", viewResult.ViewName);
            // Verifica se retornou o alterarSenhaUsuario
            Assert.NotNull(viewResult.Model);
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
                DataCadastro = DateTime.Now
            };

            return usuarioModel;
        }
        public AlterarSenhaModel alterarSenhaUsuario()
        {
            var alterarSenhaModel = new AlterarSenhaModel()
            {
                SenhaAtual = "123",
                NovaSenha = "456",
                ConfirmarNovaSenha = "456"
            };

            return alterarSenhaModel;
        }

    }
}