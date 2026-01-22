using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikCollegeTARpe24_StenUesson.Data;
using TallinnaRakenduslikCollegeTARpe24_StenUesson.Models;

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Controllers
{
    public class DelinquentController : Controller
    {
        private readonly SchoolContext _context;
        public DelinquentController(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,violtaions,teacherOrStudent,ViolationDescription")] Delinquent delinquent)
        {
            if (ModelState.IsValid)
            {
                _context.Delinquents.Add(delinquent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
                //return RedirectToAction(nameof(Index));
            }
            return View(delinquent);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delinquent = await _context.Delinquents
                .FirstOrDefaultAsync(m => m.DelinquentID == id);
            if (delinquent == null)
            {
                return NotFound();
            }

            return View(delinquent);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var delinquent = await _context.Delinquents.FirstOrDefaultAsync(d => d.DelinquentID == id);
            if (delinquent == null)
            {
                return NotFound();
            }
            return View(delinquent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,violtaions,teacherOrStudent,ViolationDescription")] Delinquent delinquent)
        {
            if (id != delinquent.DelinquentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delinquent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DelinquentExists(delinquent.DelinquentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(delinquent);
        }

        [HttpGet]
        public async Task<IActionResult> BaseOn(int? id)
        {
            if (id == null)
                return NotFound();

            var delinquent = await _context.Delinquents.FindAsync(id);
            if (delinquent == null)
                return NotFound();

            var cloneddelinquent = new Delinquent
            {
                FirstName = delinquent.FirstName,
                LastName = delinquent.LastName,
                violtaions = delinquent.violtaions,
                teacherOrStudent = delinquent.teacherOrStudent,
                ViolationDescription = delinquent.ViolationDescription,
            };

            cloneddelinquent.DelinquentID = 0;

            _context.Delinquents.Add(cloneddelinquent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delinquent = await _context.Delinquents
                .FirstOrDefaultAsync(m => m.DelinquentID == id);
            if (delinquent == null)
            {
                return NotFound();
            }

            return View(delinquent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delinquent = await _context.Delinquents.FindAsync(id);
            _context.Delinquents.Remove(delinquent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DelinquentExists(int id)
        {
            return _context.Delinquents.Any(e => e.DelinquentID == id);
        }
    }
}
