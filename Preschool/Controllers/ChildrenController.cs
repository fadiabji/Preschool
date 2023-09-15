using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IClassroomService _classroomService;
        private readonly ISubscriptionTypeService _subscriptionTypeService;

        public ChildrenController( IChildService childService, 
                                   IClassroomService classroomService, 
                                   ISubscriptionTypeService subscriptionTypeService)
        {
            _childrenService = childService;
            _classroomService = classroomService;
            _subscriptionTypeService = subscriptionTypeService;
        }

        // GET: Children
        public async Task<IActionResult> Index()
        {
              return View(await _childrenService.GetChildren());
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Children/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_classroomService.GetClasses().Result, "Id", "Name");
            ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name");
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> DocumentCopies, ChildVM childVm)
        {

            var child = new Child
            {
                Id = childVm.Id,
                FirstName = childVm.FirstName,
                LastName = childVm.LastName,
                MotherName = childVm.MotherName,
                FatherName = childVm.FatherName,
                DateOfBirth = childVm.DateOfBirth,
                EnrolDate = childVm.EnrolDate,
                ClassroomId = childVm.ClassroomId
            };
            child.Subscriptions.Add(new Subscription { SubscriptionTypeId = childVm.SubscriptionTypeId });

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
                        child.DocumentsImage = new List<DocumentsCopies>();
                    }
                    child.DocumentsImage.Add(new DocumentsCopies { ImageFile = img.FileName });
                }


                await Task.Run(() => _childrenService.EnrollChild(child));

                return RedirectToAction(nameof(Index));
            }
            return View(child);
        }



        

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
          
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
                FatherName = child.FatherName,
                MotherName = child.MotherName,
                ClassroomId = child.ClassroomId,
            };
            childVm.SubscriptionTypeId = child.Subscriptions.Select(s => s.SubscriptionTypeId).FirstOrDefault();         
            foreach ( var docuemnt in child.DocumentsImage)
            {
                childVm.DocumentCopies.Add(docuemnt.ImageFile);
            }
            ViewData["ClassId"] = new SelectList(_classroomService.GetClasses().Result, "Id", "Name");
            ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name");

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
            child.ClassroomId = childvm.ClassroomId;
            child.DocumentsImage.Clear();

            //list.Where(w => w.Name == "height").ToList().ForEach(s => s.Value = 30);

            child.Subscriptions.Where(s => s.ChildId == childvm.Id).ToList().ForEach(s => s.SubscriptionTypeId = childvm.SubscriptionTypeId);
            //child.Subscriptions.FirstOrDefault(s => s.SubscriptionTypeId == childvm.SubscriptionTypeId);
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
                            child.DocumentsImage = new List<DocumentsCopies>();
                        }
                        child.DocumentsImage.Add(new DocumentsCopies { ImageFile = img.FileName });
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
