using Microsoft.AspNetCore.Mvc;

namespace innov_web.Controllers
{
    public class TestEndPointController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
