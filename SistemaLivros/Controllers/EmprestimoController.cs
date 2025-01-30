using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SistemaLivros.Models;
using SistemaLivros.Data;

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
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimoModel emprestimos)
        {
            if(ModelState.IsValid)
            {
                _db.Emprestimos.Add(emprestimos);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        //private readonly ILogger<EmprestimoController> _logger;
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
