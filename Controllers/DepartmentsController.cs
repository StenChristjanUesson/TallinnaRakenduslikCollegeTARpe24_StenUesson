﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikCollegeTARpe24_StenUesson.Data;
using TallinnaRakenduslikCollegeTARpe24_StenUesson.Models;

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SchoolContext _context;
        public DepartmentsController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Departments.Include(d => d.Administrator);
            return View(await (schoolContext.ToListAsync()));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string query = "SELECT * FROM Departments WHERE DepartmentID = {0}";
            var department = await _context.Departments
                                            .FromSqlRaw(query, id)
                                            .Include(d => d.Administrator)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync();
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Budget, StartDate, RowVersion, InstructorID, Personality, MonthlyRevenue")] Department department)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);

        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departmentToEdit = await _context.Departments
                    .Include(i => i.Administrator)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentToEdit == null) { return NotFound(); }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", departmentToEdit.InstructorID);
            return View(departmentToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, byte[] rowVersion)
        {
            ModelState.Remove("rowVersion");
            if (id == null) { return NotFound(); }
            var departmentToUpdate = await _context.Departments.Include(i => i.Administrator)
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentToUpdate == null)
            {
                Department departmentIsDeleted = new Department();
                await TryUpdateModelAsync(departmentIsDeleted);
                ModelState.AddModelError(string.Empty, "unable to save changes. Department has already been removed.");
                ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "LastName", departmentIsDeleted.InstructorID);
                return View(departmentIsDeleted);
            }
            _context.Entry(departmentToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            var tryUpdate = await TryUpdateModelAsync<Department>(departmentToUpdate,
                "",
                s => s.Name,
                s => s.StartDate,
                s => s.Budget,
                s => s.DepartmentDescription,
                s => s.InstructorID,
                s => s.Personality,
                s => s.MonthlyRevenue);

            if (tryUpdate)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Department)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "unable to save changes, department has already been removed.");
                    }
                    else
                    {
                        var databaseValues = (Department)databaseEntry.ToObject();
                        if (databaseValues.Name != clientValues.Name) { ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}"); }
                        if (databaseValues.StartDate != clientValues.StartDate) { ModelState.AddModelError("Name", $"Current value: {databaseValues.StartDate}"); }
                        if (databaseValues.Budget != clientValues.Budget) { ModelState.AddModelError("Name", $"Current value: {databaseValues.Budget}"); }
                        if (databaseValues.Personality != clientValues.Personality) { ModelState.AddModelError("Name", $"Current value: {databaseValues.Personality}"); }
                        if (databaseValues.MonthlyRevenue != clientValues.MonthlyRevenue) { ModelState.AddModelError("Name", $"Current value: {databaseValues.MonthlyRevenue}"); }
                        if (databaseValues.DepartmentDescription != clientValues.DepartmentDescription) { ModelState.AddModelError("Name", $"Current value: {databaseValues.DepartmentDescription}"); }
                        if (databaseValues.InstructorID != clientValues.InstructorID) { ModelState.AddModelError("Name", $"Current value: {databaseValues.InstructorID}"); }
                        {
                            Instructor databaseHasThisInstructor = await _context.Instructors.FirstOrDefaultAsync(i => i.ID == databaseValues.InstructorID);
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.InstructorID}");
                        }
                        ModelState.AddModelError(string.Empty, "warning, changes you are about to save differ from the info in the DB" + "It appears this department was already" +
                            "changed after you selected the version with the old info." +
                            "click back if this new info is already correct, otherwise, click save again to oversave the department anyways.");
                        departmentToUpdate.RowVersion = databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "Fullname", departmentToUpdate.InstructorID);
            return View(departmentToUpdate);
        }

        //[HttpGet]
        //public async Task<IActionResult> BaseOn(int? id)
        //{
        //    var department = _context.Departments.Find(id);
        //    return View(department);
        //}

        //[HttpPost]
        //public async Task<IActionResult> BaseOn(Department department)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var baseonDepartment = department;
        //        _context.Departments.Add(baseonDepartment);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(department);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Make([Bind("InstructorID,Name,Budget,StartDate,Personality,MonthlyRevenue")] Department department)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakeDelete([Bind("InstructorID,Name,Budget,StartDate,Personality,MonthlyRevenue")] Department department)
        {
            if (ModelState.IsValid)
            {
                var existingDepartment = await _context.Departments.FindAsync(department.DepartmentID);

                if (existingDepartment == null)
                {
                    return NotFound();
                }
                var departmentCopy = new Department
                {
                    Name = department.Name,
                    Budget = department.Budget,
                    StartDate = department.StartDate,
                    Personality = department.Personality,
                    MonthlyRevenue = department.MonthlyRevenue,
                    InstructorID = department.InstructorID
                };

                _context.Remove(existingDepartment);
                _context.Add(departmentCopy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
