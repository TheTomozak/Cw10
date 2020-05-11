using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw10.DTOs;
using Cw10.DTOs.Requests;
using Cw10.Models;
using Microsoft.EntityFrameworkCore;

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


        public HelperRequests UpdateStudents(UpdateStudentsRequest request)
        {
            var helper = new HelperRequests();

            // var studentToUpdate = _dbContext.Student.First(student => student.IndexNumber == request.IndexNumber);
            // studentToUpdate.BirthDate = request.BirthDate;
            // studentToUpdate.FirstName = request.FirstName;
            // studentToUpdate.LastName = request.LastName;


            var countStudents = _dbContext.Student.Count(stu => stu.IndexNumber == request.IndexNumber);

            if (countStudents == 0)
            {
                helper.Number = 0;
                return helper;
            }


            var student = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate
            };

            _dbContext.Attach(student);
            // _dbContext.Entry(student).Property("IndexNumber").IsModified = true;
            _dbContext.Entry(student).Property("FirstName").IsModified = true;
            _dbContext.Entry(student).Property("LastName").IsModified = true;
            _dbContext.Entry(student).Property("BirthDate").IsModified = true;

            _dbContext.SaveChangesAsync();

            helper.Number = 1;

            return helper;
        }


        public HelperRequests DeleteStudent(DeleteStudentRequest request)
        {
            HelperRequests helperRequests = new HelperRequests();
            var countStudents = _dbContext.Student.Count(stu => stu.IndexNumber == request.IndexNumber);

            if (countStudents == 0)
            {
                helperRequests.Number = 0;
                return helperRequests;
            }

            var student = new Student
            {
                IndexNumber = request.IndexNumber
            };

            _dbContext.Attach(student);
            _dbContext.Remove(student);
            _dbContext.SaveChangesAsync();

            helperRequests.Number = 1;
            return helperRequests;
        }
    }
}