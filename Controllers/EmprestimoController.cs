using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SistemaLivros.Models;
using SistemaLivros.Data;
using System.Data;
using ClosedXML.Excel;
using SistemaLivros.SessaoService;

namespace SistemaLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        readonly private ApplicationDbContext _db;
        private readonly ISessaoInterface _sessaoInterface;
        public EmprestimoController(ApplicationDbContext db, ISessaoInterface sessaoInterface)
        {
            _db = db;
            _sessaoInterface = sessaoInterface;
        }
        public IActionResult Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            IEnumerable<EmprestimoModel> emprestimos = _db.Emprestimos;
            return View(emprestimos);
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

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
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

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

        public IActionResult Exportar()
        {
            var dados = GetDados();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(dados);
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Emprestimos.xlsx");
                }
            }
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

            if (dados.Count > 0)
            {
                dados.ForEach(emprestimo =>
                {
                    datatable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.DataEmprestimo);
                });
            }
            return datatable;
        }

            [HttpPost]
            public IActionResult Cadastrar(EmprestimoModel emprestimos)
            {
                if (ModelState.IsValid)
                {
                    emprestimos.DataEmprestimo = DateTime.Now;

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
                    var emprestimoDB = _db.Emprestimos.Find(emprestimo.Id); //para nao alterar a data tambem

                    emprestimoDB.Recebedor = emprestimo.Recebedor;
                    emprestimoDB.Fornecedor = emprestimo.Fornecedor;
                    emprestimoDB.LivroEmprestado = emprestimo.LivroEmprestado;

                    _db.Emprestimos.Update(emprestimoDB);
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
