using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "É preciso digitar a senha senha atual do usuário")]
        public string SenhaAtual { get; set; }
        
        [Required(ErrorMessage = "É preciso digitar a nova senha")]
        public string NovaSenha { get; set; }
        
        [Required(ErrorMessage = "É preciso confirmar a nova senha")]
        [Compare("NovaSenha", ErrorMessage = "A senha não confere com a nova senha")]
        public string ConfirmarNovaSenha { get; set; }

    }
}
