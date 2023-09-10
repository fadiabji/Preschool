using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;
using System.Runtime.CompilerServices;

namespace Preschool.Services
{
    public class ChildService : IChildService
    {
        private readonly ApplicationDbContext _db;
       
        public ChildService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Child>> GetChildren()
        {
            return await _db.Childern.Include(c => c.DocumentsImage).ToListAsync();
        }
        public async Task<Child> GetChildById(int? id)
        {
            return await _db.Childern.Include(c => c.DocumentsImage).SingleOrDefaultAsync(c =>c.Id == id); 
        }

        public void EnrollChild(Child child)
        {
            _db.Childern.Add(child);
            _db.SaveChanges();
        }

        public void UpdateChildEnrollment(Child child)
        {
            _db.Childern.Update(child);
            _db.SaveChanges();
        }

        public void RemoveChild(Child child)
        {
            _db.Remove(child);
            _db.SaveChanges();
        }


      

    }
}
