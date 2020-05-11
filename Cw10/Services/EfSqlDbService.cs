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
    public class EfSqlDbService : IDbService
    {
        private readonly s18969Context _dbContext;

        public EfSqlDbService(s18969Context context)
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


        public HelperRequests EnrollStudent(EnrollStudentRequest request)
        {
            HelperRequests helperRequests = new HelperRequests();
            var countStudies = _dbContext.Studies.Count(studies => studies.Name == request.Studies);

            if (countStudies == 0)
            {
                helperRequests.Number = 1;
                return helperRequests;
            }

            var latestDate = _dbContext.Enrollment.Max(date => date.StartDate);

            var countLatestEnrollment = _dbContext.Enrollment.Count(enroll =>
                enroll.IdStudy == request.IdStudy && enroll.Semester == 1 && enroll.StartDate == latestDate);

            if (countLatestEnrollment != 0)
            {
                var latestEnrollment = new Enrollment
                {
                    Semester = 1,
                    StartDate = DateTime.Now
                };
                _dbContext.Attach(latestEnrollment);
                _dbContext.SaveChangesAsync();
            }

            var countIndex = _dbContext.Student.Count(st => st.IndexNumber == request.IndexNumber);

            if (countIndex == 0)
            {
                helperRequests.Number = 2;
                return helperRequests;
            }

            var addStudent = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName =  request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate

            };

            _dbContext.Attach(addStudent);
            _dbContext.SaveChangesAsync();

            helperRequests.Number = 3;
            return helperRequests;

        }

        public void PromoteStudent(EnrollPromotionsRequest request)
        {
            _dbContext.Database.ExecuteSqlRaw("EXEC PromoteStudentProcedure @studies, @semester", request.Studies,
                request.Semester);
        }
    }
}