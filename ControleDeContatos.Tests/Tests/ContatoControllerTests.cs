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
            // Chama o index
            // Popula de conteudo

            var mockController = new Mock<ContatoController>();
            var mockObj = mockController.Object;

            // Act
            // Faz a chamada de BuscarTodos
            var lista = mockObj.Index();


            // Assert
            // Coferir se retornou conteudo
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


            
            return contatos;
        }
    }
}