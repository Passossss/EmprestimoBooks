using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SistemaLivros.Models;
using SistemaLivros.Data;
using System.Data;
using ClosedXML.Excel;
using SistemaLivros.Services.SessaoService;
using SistemaLivros.Services.EmprestimoService;

namespace SistemaLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IEmprestimosInterface _emprestimosInterface;
        public EmprestimoController(ApplicationDbContext db, 
                                            ISessaoInterface sessaoInterface,
                                            IEmprestimosInterface emprestimosInterface)
        {
            _db = db;
            _sessaoInterface = sessaoInterface;
            _emprestimosInterface = emprestimosInterface;
        }
        public async Task<IActionResult> Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var emprestimos = await _emprestimosInterface.BuscarEmprestimos();
            return View(emprestimos.Dados);
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
        public async Task<IActionResult>Editar(int? id)
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

            var emprestimo = await _emprestimosInterface.BuscarEmprestimosPorId(id);
            return View(emprestimo.Dados);
        }

        [HttpGet]
        public async Task<IActionResult>Excluir(int? id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var emprestimo = await _emprestimosInterface.BuscarEmprestimosPorId(id);

            return View(emprestimo.Dados);
        }

        public async Task<IActionResult> Exportar()
        {
            var dados = await _emprestimosInterface.BuscarDadosEmprestimosExcel();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(dados, "Dados Empréstimos");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Emprestimos.xlsx");
                }
            }
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
                TempData["MensagemErro"] = "Algum erro ocorreu ao realizar o cadastro!";
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
