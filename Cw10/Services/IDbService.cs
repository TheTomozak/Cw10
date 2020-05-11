using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw10.DTOs;
using Cw10.DTOs.Requests;
using Cw10.Models;

namespace Cw10.Services
{
   public interface IDbService
   {
       public IEnumerable<Student> GetStudents();

       public HelperRequests DeleteStudent(DeleteStudentRequest request);

       public HelperRequests UpdateStudents(UpdateStudentsRequest request);

       HelperRequests EnrollStudent(EnrollStudentRequest request);
       void PromoteStudent(EnrollPromotionsRequest request);
    }
}
