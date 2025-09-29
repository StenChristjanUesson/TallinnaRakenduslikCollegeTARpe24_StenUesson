﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikCollegeTARpe24_StenUesson.Data;
using TallinnaRakenduslikCollegeTARpe24_StenUesson.Models;

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
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["RequestedView"] = "Create";
            return View("CreateEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Title,Credits")] Course course)
        {
            ModelState.Remove("CourseID");
            int lastid = await _context.Courses.CountAsync();
            course.CourseID += lastid++;
            if (ModelState.IsValid)
            {

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }
        //Edit controller
        [HttpGet]
        public IActionResult Edit([Bind("CourseID,Title,Credits")] int? id)
        {
            ViewData["RequestedView"] = "Edit";
            Course course = _context.Courses.Find(id);
            return View("CreateEdit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("CourseID,Title,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("CreateEdit", course);
        }
        //Clone controller
        public async Task<ActionResult> Clone(int? id, Course course)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);

            var CourseClone = new Course();
            CourseClone.Credits = Course.Credits;
            CourseClone.Title = Course.Title;
            ModelState.Remove("CourseID");
            if (ModelState.IsValid)
            {
                _context.Courses.Add(CourseClone);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        //Details controller
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["RequestedView"] = "Details";
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }
            return View("DetailsDelete", course);
        }

        //Delete controller
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["RequestedView"] = "Delete";
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }
            return View("DetailsDelete", course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
