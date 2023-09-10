using Preschool.Models;

namespace Preschool.Services
{
    public class ClassService : IClassService
    {
        public Task<Child> GetClassById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Child>> GetClasses()
        {
            throw new NotImplementedException();
        }
    }
}
