using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace SistemaLivros.Models
{
    public class EmprestimoModel
    {
        public int Id { get; set; }
        public string Recebedor { get; set; }
        public string Fornecedor { get; set; }
        public string LivroEmprestado { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now;
    
    }
}
