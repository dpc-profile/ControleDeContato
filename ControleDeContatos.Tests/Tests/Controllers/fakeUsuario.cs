using System;
using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Tests.Tests.Controllers
{
    public static class fakeUsuario
    {
        public static UsuarioModel ModeloDadosUsuario()
        {
            return new UsuarioModel()
            {
                Id = 1,
                Nome = "Padronos Tester",
                Login = "padronos",
                Email = "padronos@gmail.com",
                Senha = "2e6f9b0d5885b6010f9167787445617f553a735f",
                Perfil = Enums.PerfilEnums.Padrao,
                DataCadastro = DateTime.Now

            };
        }

        public static List<UsuarioModel> VariosUsuarios()
        {
            List<UsuarioModel> usuarios = new List<UsuarioModel>();

            usuarios.Add(new UsuarioModel()
            {   
                Id = 2,
                Nome = "Paula Tester",
                Login = "paulaT",
                Email = "paula@gmail.com",
                Senha = "2e6f9b0d5885b6010f9167787445617f553a735f",
                Perfil = Enums.PerfilEnums.Padrao,
                DataCadastro = DateTime.Now
            });
            
            usuarios.Add(new UsuarioModel()
            {   
                Id = 3,
                Nome = "Thomas Tester",
                Login = "thomasT",
                Email = "thoma@gmail.com",
                Senha = "2e6f9b0d5885b6010f9167787445617f553a735f",
                Perfil = Enums.PerfilEnums.Padrao,
                DataCadastro = DateTime.Now
            });

            return usuarios;
        }
        public static AlterarSenhaModel ModeloAlterarSenhaUsuario()
        {
            return new AlterarSenhaModel()
            {
                SenhaAtual = "123",
                NovaSenha = "456",
                ConfirmarNovaSenha = "456"
            };
        }

        public static UsuarioSemSenhaModel ModeloUsuarioSemSenha()
        {
            return new UsuarioSemSenhaModel()
            {
                Id = 1,
                Nome = "Padronos Tester",
                Login = "padronos",
                Email = "padronos@gmail.com",
                Perfil = Enums.PerfilEnums.Padrao,
            };
        }
    }
}