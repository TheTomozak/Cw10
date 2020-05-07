using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw10.Models;

namespace Cw10.Services
{
    public class EfStudentDbService : IStudentDbService
    {
        private readonly s18969Context _dbContext;

        public EfStudentDbService(s18969Context context)
        {
            _dbContext = context;
        }


        public IEnumerable<Student> GetStudents()
        {
            var listStudents = _dbContext.Student.ToList();

            return listStudents;
        }
    }
}
