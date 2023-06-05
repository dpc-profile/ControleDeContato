using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Tests.Tests.Controllers;

using Moq;

using Xunit;

namespace ControleDeContatos.Tests.Tests.Repository
{
    public class UsuarioRepositoryTests : IDisposable
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioRepositoryTests()
        {
            _usuarioRepository = new UsuarioRepository();

            _usuarioRepository.CreateSavepointAsync();

            Setup_OrganizarPreTeste();

        }

        public void Dispose()
        {
            Setup_Reverter();
        }

        [Fact]
        public void TestarBuscarTodos()
        {
            var result = _usuarioRepository.BuscarTodos();

            Assert.IsType<List<UsuarioModel>>(result);

            Assert.True(result.Count != 0);
        }

        [Fact]
        public void TestarBuscarPorLogin()
        {
            var result = _usuarioRepository.BuscarPorLogin(
                fakeUsuario.UsuarioModel_Database().Login);

            Assert.NotNull(result);
        }
    
        [Fact]
        public void TestarBuscarPorLogin_DeveRetornarNulo()
        {
            var result = _usuarioRepository.BuscarPorLogin("");

            Assert.Null(result);
        }

        [Fact]
        public void TestarBuscarPorEmail()
        {
            var result = _usuarioRepository.BuscarPorEmail(
                fakeUsuario.UsuarioModel_Database().Email);

            Assert.NotNull(result);
        }
    
        [Fact]
        public void TestarBuscarPorEmail_DeveRetornarNulo()
        {
            var result = _usuarioRepository.BuscarPorEmail("");

            Assert.Null(result);
        }

        [Fact]
        public void TestarAdicionarException()
        {
            var message = Assert.Throws<Exception>(
                () => _usuarioRepository.Adicionar(It.IsAny<UsuarioModel>())
            );
            
            Assert.Equal("Erro ao adicionar o usuário do banco de dados", message.Message);

        }

        [Fact]
        public void TestarApagar_Exception()
        {
            var message = Assert.Throws<Exception>(
                () => _usuarioRepository.Apagar(It.IsAny<UsuarioModel>())
            );
            
            Assert.Equal("Erro ao apagar o usuário do banco de dados", message.Message);
            

        }
        private void Setup_OrganizarPreTeste()
        {
            _usuarioRepository.Adicionar(fakeUsuario.UsuarioModel_Database());
        }
        private void Setup_Reverter()
        {
            _usuarioRepository.RollbackAsync();
        }
    }
}