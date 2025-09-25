using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikCollegeTARpe24_StenUesson.Data;

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;
        public CoursesController(SchoolContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var courses = _context.Courses.Include(c => c.Department).AsNoTracking();
            return View();
        }
    }
}
