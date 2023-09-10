using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
//using AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;
using Preschool.Models.ViewModels;
using Preschool.Services;

namespace Preschool.Controllers
{
    public class ChildrenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IChildService _childrenService;

        public ChildrenController(ApplicationDbContext context, IChildService childService)
        {
            _context = context;
            _childrenService = childService;
        }

        // GET: Children
        public async Task<IActionResult> Index()
        {
              return View(await _childrenService.GetChildren());
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _childrenService.GetChildren() == null)
            {
                return NotFound();
            }

            var child = await _childrenService.GetChildById(id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // GET: Children/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> DocumentCopies, Child child)
        {
            if (ModelState.IsValid && DocumentCopies != null)
            {
                
                foreach (var img in DocumentCopies)
                {
                    string fileName = img.FileName;
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DocumentsCopies"));
                    using (var filestream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    { await img.CopyToAsync(filestream); }
                    if (child.DocumentsImage == null)
                    {
                        child.DocumentsImage = new List<DocumentsImage>();
                    }
                    child.DocumentsImage.Add(new DocumentsImage { ImageFile = img.FileName });
                }


                await Task.Run(() => _childrenService.EnrollChild(child));

                return RedirectToAction(nameof(Index));
            }
            return View(child);
        }



        

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Childern == null)
            {
                return NotFound();
            }

            var child = await _context.Childern.FindAsync(id);
            if (child == null)
            {
                return NotFound();
            }
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,FatherName,MotherName,DateOfBirth,EnrolDate,IsActive")] Child child)
        {
            if (id != child.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(child);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildExists(child.Id))
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
            return View(child);
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Childern == null)
            {
                return NotFound();
            }

            var child = await _context.Childern
                .FirstOrDefaultAsync(m => m.Id == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Childern == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Childern'  is null.");
            }
            var child = await _context.Childern.FindAsync(id);
            if (child != null)
            {
                _context.Childern.Remove(child);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildExists(int id)
        {
          return _context.Childern.Any(e => e.Id == id);
        }
    }
}
