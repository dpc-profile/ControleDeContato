using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ControleDeContatos.Models
{
    [ExcludeFromCodeCoverage]
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do contato é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email do contato é obrigatório")]
        [EmailAddress(ErrorMessage = "O email não é valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O número do contato é obrigatório")]
        [Phone(ErrorMessage = "O número do contato não é valido.")]
        public string Celular { get; set; }
        
        public int UsuarioId { get; set; }

        public UsuarioModel Usuario { get; set; }
    }
}
