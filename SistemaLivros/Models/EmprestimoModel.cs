using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace SistemaLivros.Models
{
    public class EmprestimoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Recebedor' é obrigatório.")]
        public string Recebedor { get; set; }

        [Required(ErrorMessage = "O campo 'Fornecedor' é obrigatório.")]
        public string Fornecedor { get; set; }

        [Required(ErrorMessage = "O nome do 'Livro' é obrigatório.")]
        public string LivroEmprestado { get; set; }
        public DateTime DataEmprestimo { get; set; }= DateTime.Now;

    }
}
