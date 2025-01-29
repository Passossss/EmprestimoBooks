using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SistemaLivros.Models;

namespace SistemaLivros.Controllers
{
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
