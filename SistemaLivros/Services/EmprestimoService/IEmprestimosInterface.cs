using System.Data;
using SistemaLivros.Models;

namespace SistemaLivros.Services.EmprestimoService
{
    public interface IEmprestimosInterface
    {
        Task<ResponseModel<List<EmprestimoModel>>> BuscarEmprestimos();
        Task<ResponseModel<EmprestimoModel>> BuscarEmprestimosPorId(int? id);
        Task<DataTable> BuscarDadosEmprestimosExcel();
        Task<ResponseModel<EmprestimoModel>> CadastrarEmprestimo(EmprestimoModel emprestimoModel);
    }
}
