using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using ControleDeContatos.Enums;

namespace ControleDeContatos.Models
{
    [ExcludeFromCodeCoverage]
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public string Senha { get; set; }
        
    }
}
