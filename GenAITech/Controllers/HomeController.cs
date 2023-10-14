using GenAITech.Data;
using GenAITech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GenAITech.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

            
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            
        }

        public async Task<IActionResult> IndexAsync()
        {
            return _context.GenAI != null ?
                         View(await _context.GenAI.ToListAsync()) :
                         Problem("Entity set 'ApplicationDbContext.GenAI'  is null.");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult GenAiTools()
        {
            return View();
        }

        public IActionResult GenAiOrganizasion()
        {
            return View();
        }

        public IActionResult GenAiApplications()
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