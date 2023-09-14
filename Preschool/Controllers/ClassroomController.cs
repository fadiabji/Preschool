using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;
using Preschool.Services;

namespace Preschool.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classService)
        {
            _classroomService = classService;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            return View(await _classroomService.GetClasses());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _classroomService.GetClasses() == null)
            {
                return NotFound();
            }

            var @class = await _classroomService.GetClassById(id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Classroom @class)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(()=> _classroomService.CreateClass(@class));
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _classroomService.GetClasses() == null)
            {
                return NotFound();
            }

            var @class =  await _classroomService.GetClassById(id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Classroom @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(()=>_classroomService.UpdateClass(@class));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Id))
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
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _classroomService.GetClasses() == null)
            {
                return NotFound();
            }

            var @class = await _classroomService.GetClassById(id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var @class = await _classroomService.GetClassById(id);
                if (@class != null)
                {
                    _classroomService.RemoveClass(@class);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private bool ClassExists(int id)
        {
          return _classroomService.IsExists(id);
        }
    }
}
