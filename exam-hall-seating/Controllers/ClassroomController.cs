using Microsoft.AspNetCore.Mvc;

namespace exam_hall_seating.Controllers
{
    public class ClassroomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
