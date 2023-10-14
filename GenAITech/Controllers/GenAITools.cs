using Microsoft.AspNetCore.Mvc;

namespace GenAITech.Controllers
{
    public class GenAITools : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChatGPT()
        {
            return View();
        }
        public IActionResult Midjourney()
        {
            return View();
        }
        public IActionResult DALLE()
        {
            return View();
        }
        public IActionResult GoogleBRAD()
        {
            return View();
        }

    }
}
