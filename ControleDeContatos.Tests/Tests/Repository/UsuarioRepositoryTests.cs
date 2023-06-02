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

            FazerSetup();

        }

        public void Dispose()
        {
            LimparSetup();
        }

        [Fact]
        public void TestarBuscarTodos()
        {
            var result = _usuarioRepository.BuscarTodos();

            Assert.IsType<List<UsuarioModel>>(result);

            Assert.True(result.Count == 1);
        }

        private void FazerSetup()
        {
            // Cria um contato no banco de dados
            _usuarioRepository.Adicionar(fakeUsuario.UsuarioModel_Database());

        }

        private void LimparSetup()
        {
            // Limpar o db de teste
            var usuarios = _usuarioRepository.BuscarTodos();

            foreach (var usuario in usuarios)
            {
                _usuarioRepository.Apagar(usuario);
            }

        }
    }
}