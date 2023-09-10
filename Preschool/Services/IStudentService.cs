using Preschool.Models;
using System.Runtime.CompilerServices;

namespace Preschool.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentById(int id);
       
    }
}
