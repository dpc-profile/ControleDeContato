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
            // Arrange
            var result = _usuarioRepository.BuscarPorEmail(
                fakeUsuario.UsuarioModel_Database().Email);

            // Act
            Assert.NotNull(result);
        }

        [Fact]
        public void TestarBuscarPorEmail_DeveRetornarNulo()
        {
            // Arrange
            var result = _usuarioRepository.BuscarPorEmail("");

            // Act
            Assert.Null(result);
        }

        [Fact]
        public void TestarAdicionar()
        {
            UsuarioModel usuario = new UsuarioModel(){
                Id = 3,
                Nome = "Patricia Tester",
                Login = "patricia",
                Email = "patricia@gmail.com",
                Senha = "2e6f9b0d5885b6010f9167787445617f553a735f",
                Perfil = Enums.PerfilEnums.Padrao,
                DataCadastro = DateTime.Now
            };

            _usuarioRepository.Adicionar(usuario);
        }

        [Fact]
        public void TestarAdicionarException()
        {
            // Assert
            var message = Assert.Throws<Exception>(
                // Act
                () => _usuarioRepository.Adicionar(It.IsAny<UsuarioModel>())
            );

            Assert.Equal("Erro ao adicionar o usuário do banco de dados", message.Message);

        }
       
        [Fact]
        public void TestarApagar_Exception()
        {
            // Assert
            var message = Assert.Throws<Exception>(
                // Act
                () => _usuarioRepository.Apagar(It.IsAny<UsuarioModel>())
            );

            Assert.Equal("Erro ao apagar o usuário do banco de dados", message.Message);
        }

        [Fact]
        public void TestarAtualizar()
        {
            // Arrange
            UsuarioModel usuarioDb = _usuarioRepository.ListarPorId(
                fakeUsuario.UsuarioModel_Database().Id);

            usuarioDb.Nome = "Tester";
            usuarioDb.Email = "amandinha@gmail.com";
            usuarioDb.Login = "aaa";
            usuarioDb.Perfil = Enums.PerfilEnums.Admin;
            usuarioDb.DataAtualizacao = DateTime.Now;

            // Act
            _usuarioRepository.Atualizar(usuarioDb);
        }

        [Fact]
        public void TestarAtualizar_Exception()
        {
            // Assert
            var message = Assert.Throws<Exception>(
                // Act
                () => _usuarioRepository.Atualizar(It.IsAny<UsuarioModel>())
            );

            Assert.Equal("Erro ao atualizar o usuário no banco de dados", message.Message)            ;
        }

        private void Setup_OrganizarPreTeste()
        {

            UsuarioModel usuariosDb = _usuarioRepository.ListarPorId(
                fakeUsuario.UsuarioModel_Database().Id);
            
            // O usuario já deveria estar cadastrado, mas se não tiver faz
            if (usuariosDb == null) _usuarioRepository.Adicionar(fakeUsuario.UsuarioModel_Database());

            _usuarioRepository.CreateSavepointAsync();
            
        }

        private void Setup_Reverter()
        {
            _usuarioRepository.RollbackAsync();
        }
    }
}