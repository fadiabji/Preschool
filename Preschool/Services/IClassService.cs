using Preschool.Models;
using System.Runtime.CompilerServices;

namespace Preschool.Services
{
    public interface IClassService
    {
        Task<IEnumerable<Child>> GetClasses();
        Task<Child> GetClassById(int id);
       
    }
}
