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

namespace ControleDeContatos.Tests.Tests
{
    public class ContatoControllerTests
    {

        [Fact]
        public void TestaIndex()
        {
            // Arrange
            // Instancia o mock do repository
            // Popula o mock com o CriaTestContatos()
            // Instanciar o controller usando o obj do mockRepo
            var mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(repo => repo.BuscarTodos())
                .Returns(CriaTestContatos());
            var controller = new ContatoController(mockRepo.Object);

            // Act
            // Faz a chamada do index
            var result = controller.Index();
            
            // Assert
            // Confere o se o tipo é viewResult
            // Confere o viewResul se está com o model
            // Comfere se a model está com os 2 contatos adicionados
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ContatoModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }


        private List<ContatoModel> CriaTestContatos()
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
    }
}