using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SistemaLivros.Dto
{
    public class UsuarioRegisterDto
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo 'Sobrenome' é obrigatório.")]
        public string Sobrenome { get; set; }
        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo 'Senha' é obrigatório.")]
        public byte[] Senha { get; set; }
        [Required(ErrorMessage = "O campo 'Confirma Senha' é obrigatório."),
            Compare("Senha", ErrorMessage = "As senhas não sao iguais")]
        public byte[] ConfirmaSenha { get; set; }
    
    }
}
