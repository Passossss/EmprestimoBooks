using System.ComponentModel.DataAnnotations;

namespace SistemaLivros.Dto
{
    public class UsuarioLoginDto
    {

        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite a senha!!")]
        public string Senha { get; set; }

    }
}
