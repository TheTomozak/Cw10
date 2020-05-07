using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw10.Models;

namespace Cw10.Services
{
   public interface IStudentDbService
   {
       public IEnumerable<Student> GetStudents();
   }
}
