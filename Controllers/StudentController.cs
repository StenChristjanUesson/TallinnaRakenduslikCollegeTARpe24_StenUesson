using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikCollegeTARpe24_StenUesson.Data;

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolContext _context;
        public StudentController(SchoolContext context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }
    }
}
