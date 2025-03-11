using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SistemaLivros.Models;
using SistemaLivros.Data;
using System.Data;
using ClosedXML.Excel;

namespace SistemaLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        readonly private ApplicationDbContext _db;
        public EmprestimoController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<EmprestimoModel> emprestimos = _db.Emprestimos;
            return View(emprestimos);
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimoModel emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if (id == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            EmprestimoModel emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }
            return View(emprestimo);
        }

        public IActionResult Exportar(EmprestimoModel emprestimo) 
        {
            var dados = GetDados();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(dados);
                wb.SaveAs("Emprestimos.xlsx");
            }

            return Ok();
        }

        private DataTable GetDados()
        {
            DataTable datatable = new DataTable();
            datatable.TableName = "Dados Emprestimos";
            datatable.Columns.Add("Recebedor", typeof(string));
            datatable.Columns.Add("Fornecedor", typeof(string));
            datatable.Columns.Add("Livro", typeof(string));
            datatable.Columns.Add("Data Emprestimo", typeof(DateTime));

            var dados = _db.Emprestimos.ToList();

            if(dados.Count > 0)
            {
                dados.ForEach(emprestimo =>
                {
                    datatable.Rows.Add(new object[] { emprestimo.Recebedor, emprestimo.Fornecedor,emprestimo.LivroEmprestado, emprestimo.DataUltimaAtualizacao });)
            }
            return datatable;
        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimoModel emprestimos)
        {
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Add(emprestimos);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";

                return RedirectToAction("Index");
            }
            //TempData["MensagemErro"] = "Algum erro ocorreu ao realizar o cadastro!";
            return View();
        }
        [HttpPost]
        public IActionResult Editar(EmprestimoModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Update(emprestimo);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Edição realizada com sucesso!";

                return RedirectToAction("Index");
            }
            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar a edição!";
            return View(emprestimo);

        }
        [HttpPost]
        public IActionResult Excluir(EmprestimoModel emprestimo)
        {
            if (emprestimo == null)
            {
                return NotFound();
            }
            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();

            TempData["MensagemSucesso"] = "Remoção realizada com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
