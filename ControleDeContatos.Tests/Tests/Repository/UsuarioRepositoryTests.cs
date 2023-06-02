using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Tests.Tests.Controllers;

using Xunit;

namespace ControleDeContatos.Tests.Tests.Repository
{
    public class UsuarioRepositoryTests : IDisposable
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioRepositoryTests()
        {
            _usuarioRepository = new UsuarioRepository();

            OrganizarPreTeste();

        }

        public void Dispose()
        {
            LimparPosTeste();
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

        private void OrganizarPreTeste()
        {
            _usuarioRepository.Adicionar(fakeUsuario.UsuarioModel_Database());
        }

        private void LimparPosTeste()
        {
            // Limpar o db de teste
            var usuarios = _usuarioRepository.BuscarTodos();

            foreach (var usuario in usuarios)
            {
                if (usuario.Id != fakeUsuario.UsuarioModelParaContatos_Database().Id)
                    _usuarioRepository.Apagar(usuario);
            }

        }
    }
}