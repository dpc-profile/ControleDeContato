using System;
using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Tests.Tests.Controllers
{
    public static class fakeUsuario
    {
        public static UsuarioModel UsuarioModel_Database()
        {
            // Senha teste
            return new UsuarioModel()
            {
                Id = 2,
                Nome = "Padronos Tester",
                Login = "padronos",
                Email = "padronos@gmail.com",
                Senha = "2e6f9b0d5885b6010f9167787445617f553a735f",
                Perfil = Enums.PerfilEnums.Padrao,
                DataCadastro = DateTime.Now

            };
        }
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
                SenhaAtual = "teste",
                NovaSenha = "novaSenha",
                ConfirmarNovaSenha = "novaSenha"
            };
        }
        public static AlterarSenhaModel ModeloInvalidoAlterarSenhaUsuario_SenhaNaoConfere()
        {
            return new AlterarSenhaModel()
            {
                SenhaAtual = "outroTeste",
                NovaSenha = "novaSenha",
                ConfirmarNovaSenha = "novaSenha"
            };
        }
        public static AlterarSenhaModel ModeloInvalidoAlterarSenhaUsuario_SenhaIguais()
        {
            return new AlterarSenhaModel()
            {
                SenhaAtual = "teste",
                NovaSenha = "teste",
                ConfirmarNovaSenha = "teste"
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
        public static RedefinirSenhaModel ModeloRedefinirSenha()
        {
            return new RedefinirSenhaModel()
            {
                Login = "padronos",
                Email = "padronos@gmail.com"
            };
        }
        public static LoginModel ModeloLoginValido()
        {
            return new LoginModel()
            {
                Login = "padronos",
                Senha = "teste"
            };
        }
        public static UsuarioModel ModeloDadosUsuario_SenhaInvalida()
        {
            // A senha não está convertida em hash
            return new UsuarioModel()
            {
                Id = 1,
                Nome = "Padronos Tester",
                Login = "padronos",
                Email = "padronos@gmail.com",
                Senha = "teste",
                Perfil = Enums.PerfilEnums.Padrao,
                DataCadastro = DateTime.Now
            };
        }
    }
}