using Microsoft.AspNetCore.Mvc;

namespace Marie.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
