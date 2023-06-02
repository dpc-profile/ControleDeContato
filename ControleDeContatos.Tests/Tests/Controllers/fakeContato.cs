using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Tests.Tests.Controllers
{
    public static class fakeContato
    {
        public static List<ContatoModel> VariosContatoModel_Database()
        {

            var contatos = new List<ContatoModel>();

            contatos.Add(new ContatoModel()
            {
                Id = 1,
                Nome = "Carlos Tester1",
                Email = "carlos1@teste.com",
                Celular = "11 98765-1234",
                UsuarioId = fakeUsuario.UsuarioModel_Database().Id,
            });

            contatos.Add(new ContatoModel()
            {
                Id = 2,
                Nome = "Carlos Tester2",
                Email = "carlos2@teste.com",
                Celular = "11 98765-1234",
                UsuarioId = fakeUsuario.UsuarioModel_Database().Id,
            });
            
            contatos.Add(new ContatoModel()
            {
                Id = 3,
                Nome = "Carlos Tester3",
                Email = "carlos3@teste.com",
                Celular = "11 98765-1234",
                UsuarioId = fakeUsuario.UsuarioModel_Database().Id,
            });

            return contatos;
        }
        public static List<ContatoModel> VariosContatos()
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
        public static ContatoModel UmContato()
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