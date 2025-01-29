using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SistemaLivros.Models;

namespace SistemaLivros.Controllers
{
    public class EmprestimoControllers
    {
        public void EmprestarLivro(string recebedor, string fornecedor, string livroEmprestado)
        {
            EmprestimoModel emprestimo = new EmprestimoModel();
            emprestimo.Recebedor = recebedor;
            emprestimo.Fornecedor = fornecedor;
            emprestimo.LivroEmprestado = livroEmprestado;
            emprestimo.DataEmprestimo = DateTime.Now;
        }
    }
    public class EmprestimoController : Controller
    {
        private readonly ILogger<EmprestimoController> _logger;

        public EmprestimoController(ILogger<EmprestimoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
