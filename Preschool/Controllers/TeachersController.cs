﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;
using Preschool.Models.ViewModels;
using Preschool.Services;

namespace Preschool.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IClassroomService _classroomService;
        public TeachersController(ITeacherService teacherService, IClassroomService classroomService)
        {
            _teacherService = teacherService;
            _classroomService = classroomService;
        }

        // GET: Teacherren
        public async Task<IActionResult> Index()
        {
            return View(await _teacherService.GetTeachers());
        }

        // GET: Teacherren/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _teacherService.GetTeachers() == null)
            {
                return NotFound();
            }

            var teacher = await _teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teacherren/Create
        public IActionResult Create()
        {

            ViewData["ClassId"] = new SelectList( _classroomService.GetClasses().Result, "Id", "Name");
            return View();
        }

        // POST: Teacherren/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> DocumentCopies, Teacher teacher)
        {
            if (ModelState.IsValid && DocumentCopies != null)
            {

                foreach (var img in DocumentCopies)
                {
                    string fileName = img.FileName;
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DocumentsCopies"));
                    using (var filestream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    { await img.CopyToAsync(filestream); }
                    if (teacher.DocumentsImage == null)
                    {
                        teacher.DocumentsImage = new List<DocumentsCopies>();
                    }
                    teacher.DocumentsImage.Add(new DocumentsCopies { ImageFile = img.FileName });
                }


                await Task.Run(() => _teacherService.RegistTeacher(teacher));

                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassroomId"] = new SelectList(await Task.Run(() => _classroomService.GetClasses().Result), "Id", "Name", teacher.ClassroomId);
            return View(teacher);
        }





        // GET: Teacherren/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           

            var teacher = await _teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            TeacherVM teacherVm = new TeacherVM()
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                DateOfBirth = teacher.DateOfBirth,
                RegistedAt = teacher.RegistedAt,
                IsActive = teacher.IsActive,
                ClassroomId = teacher.ClassroomId,  
            };

            foreach (var docuemnt in teacher.DocumentsImage)
            {
                teacherVm.DocumentCopies.Add(docuemnt.ImageFile);
            }
            ViewData["ClassId"] = new SelectList(_classroomService.GetClasses().Result, "Id", "Name");
            return View(teacherVm);
        }

        // POST: Teacherren/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherVM teachervm, List<IFormFile> DocumentCopies)
        {
            var teacher = _teacherService.GetTeacherById(id).Result;
            teacher.Id = teachervm.Id;
            teacher.FirstName = teachervm.FirstName;
            teacher.LastName = teachervm.LastName;
            teacher.DateOfBirth = teachervm.DateOfBirth;
            teacher.IsActive = teachervm.IsActive;
            teacher.ClassroomId = teachervm.ClassroomId;
            teacher.DocumentsImage.Clear();

            if (id != teacher.Id)
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
                        if (teacher.DocumentsImage == null)
                        {
                            teacher.DocumentsImage = new List<DocumentsCopies>();
                        }
                        teacher.DocumentsImage.Add(new DocumentsCopies { ImageFile = img.FileName });
                    }
                    await Task.Run(() => _teacherService.UpdateTeacherRegistration(teacher));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
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
            return View(teacher);
        }

        // GET: Teacherren/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherById(id);
                if (teacher == null)
                {
                    return NotFound();
                }

                return View(teacher);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Teacherren/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherById(id);
                if (_teacherService.IsExists(id))
                {
                    _teacherService.RemoveTeacher(teacher);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        private bool TeacherExists(int id)
        {
            return _teacherService.GetTeacherById(id) != null;
        }
    }
}
