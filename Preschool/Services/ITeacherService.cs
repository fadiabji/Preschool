using Preschool.Models;
using System.Runtime.CompilerServices;

namespace Preschool.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<Child>> GetTeachers();
        Task<Child> GetTeacherById(int id);
       
    }
}
