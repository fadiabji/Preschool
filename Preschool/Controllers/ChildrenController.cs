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
        private readonly IChildService _childrenService;

        public ChildrenController( IChildService childService)
        {
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
            if (id == null || _childrenService.GetChildren() == null)
            {
                return NotFound();
            }

            var child = await _childrenService.GetChildById(id);
            if (child == null)
            {
                return NotFound();
            }
            ChildVM childVm = new ChildVM()
            {
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
                DateOfBirth = child.DateOfBirth,
                EnrolDate = child.EnrolDate,
                IsActive = child.IsActive,
                FatherName = child.FatherName,
                MotherName = child.MotherName
            };
         
            foreach ( var docuemnt in child.DocumentsImage)
            {
                childVm.DocumentCopies.Add(docuemnt.ImageFile);
            }

            return View(childVm);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ChildVM childvm, List<IFormFile> DocumentCopies)
        {
            var child = _childrenService.GetChildById(id).Result;
            child.Id = childvm.Id;
            child.FirstName = childvm.FirstName;
            child.LastName = childvm.LastName;
            child.FatherName = childvm.FatherName;
            child.MotherName = childvm.MotherName;
            child.DateOfBirth = childvm.DateOfBirth;
            child.EnrolDate = childvm.EnrolDate;
            child.IsActive = childvm.IsActive;
            child.DocumentsImage.Clear();

            if (id != child.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && DocumentCopies != null)
            {
                try
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
                    await Task.Run(() => _childrenService.UpdateChildEnrollment(child));
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
            try
            {
                var child = await _childrenService.GetChildById(id);
                if (child == null)
                {
                    return NotFound();
                }

                return View(child);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var child = await _childrenService.GetChildById(id);
                if (_childrenService.IsExists(id))
                {
                    _childrenService.RemoveChild(child);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private bool ChildExists(int id)
        {
          return _childrenService.GetChildById(id) != null;
        }
    }
}
