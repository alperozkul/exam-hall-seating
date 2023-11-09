using Microsoft.AspNetCore.Mvc;

namespace exam_hall_seating.Controllers
{
    public class EnrollmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
