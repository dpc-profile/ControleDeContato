using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Services;

using Moq;

using Xunit;

namespace ControleDeContatos.Tests.Tests.Controllers.Services
{
    public class UsuarioServicesTests
    {
        [Fact]
        public void TestarAdicionarUsuario()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(s => s.BuscarPorLogin(It.IsAny<string>()));

            var services = new UsuarioServices(mockRepo.Object);

            // Act
            services.AdicionarUsuario(It.IsAny<UsuarioModel>());
        }
        
        [Fact]
        public void TestarAdicionarUsuario_Exception_LoginJaCadastrado()
        {
            throw new NotImplementedException();
        }
        
        [Fact]
        public void TestarAdicionarUsuario_Exception_EmailJaCadastrado()
        {
            throw new NotImplementedException();
        }

    }
}