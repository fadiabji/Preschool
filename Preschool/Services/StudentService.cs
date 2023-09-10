using Preschool.Models;

namespace Preschool.Services
{
    public class StudentService : IStudentService
    {
        public Task<Student> GetStudentById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetStudents()
        {
            throw new NotImplementedException();
        }
    }
}
