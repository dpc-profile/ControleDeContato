using System;
using System.ComponentModel.DataAnnotations;

using ControleDeContatos.Enums;
using ControleDeContatos.Helper;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O login do usuário é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O email do usuário é obrigatório")]
        [EmailAddress(ErrorMessage = "O email não é valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A seleção do perfil do usuário é obrigatório")]
        public PerfilEnums? Perfil { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatório")]
        public string Senha {get; set;}
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao{ get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }
    }
}
