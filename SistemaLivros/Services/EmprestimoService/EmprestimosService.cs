using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using SistemaLivros.Data;
using SistemaLivros.Models;

namespace SistemaLivros.Services.EmprestimoService
{
    public class EmprestimosService : IEmprestimosInterface
    {
        private readonly ApplicationDbContext _context;
        public EmprestimosService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataTable> BuscarDadosEmprestimosExcel() {

            DataTable datatable = new DataTable();
            datatable.TableName = "Dados Emprestimos";
            datatable.Columns.Add("Recebedor", typeof(string));
            datatable.Columns.Add("Fornecedor", typeof(string));
            datatable.Columns.Add("Livro", typeof(string));
            datatable.Columns.Add("Data Emprestimo", typeof(DateTime));

            var emprestimos = await BuscarEmprestimos();

            if (emprestimos.Dados.Count > 0)
            {
                emprestimos.Dados.ForEach(emprestimo =>
                {
                    datatable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.DataEmprestimo);
                });
            }
            return datatable;
        }
        
        public async Task<ResponseModel<EmprestimoModel>> CadastrarEmprestimo(EmprestimoModel emprestimosModel)
        {
            ResponseModel<EmprestimoModel> response = new ResponseModel<EmprestimoModel>();

            try
            {

                _context.Add(emprestimosModel);
                await _context.SaveChangesAsync();

                response.Mensagem = "Cadastro realizado com sucesso!";

                return response;

            }catch(Exception ex)
            {
                response.Mensagem= ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<EmprestimoModel>>> BuscarEmprestimos()
        {
            ResponseModel<List<EmprestimoModel>> response = new ResponseModel<List<EmprestimoModel>>();

            try
            {
                var emp = await _context.Emprestimos.ToListAsync();
                response.Dados = emp;
                response.Mensagem= "Dados retornado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status= false;
                return response;
            }

        }

        public async Task<ResponseModel<EmprestimoModel>> BuscarEmprestimosPorId(int? id)
        {
            ResponseModel<EmprestimoModel> response = new ResponseModel<EmprestimoModel>();
            try
            {
                if (id == null)
                {
                    response.Mensagem = "Empréstimo não encontrado";
                    response.Status = false;
                    return response;
                }

               var emp = await _context.Emprestimos.FirstOrDefaultAsync(x => x.Id == id);

               if(emp == null)
                {
                    response.Mensagem = "Empréstimo não encontrado";
                    response.Status = false;
                    return response;
                }
                response.Dados = emp;
                response.Mensagem = "Dados coletados com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }


        }
    }
}
