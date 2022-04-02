using HeirsHolding.Core.Entities;
using HeirsHolding.Core.Interfaces.Database;
using HeirsHolding.Core.Interfaces.Services;
using HeirsHolding.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Infrastructure.Services
{
    public class CourseService : ICourseService
    {
        private readonly IDataSource _dataSource;
        public CourseService(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public async Task<List<Courses>> GetAllCourses()
        {
            var res = await _dataSource.Get<Courses>(nameof(Courses));
            return res;
        }
               
        public async Task<Courses> GetCourseById(string courseId)
        {
            var courses = await _dataSource.Get<Courses>(nameof(Courses));
            var course = courses.FirstOrDefault(x => x.Id == courseId);
            if (course == null) throw new Exception("Course Not Found");
            return course;
        }
               
        public async Task<List<Courses>> UploadCourses(List<Courses> course)
        {
            var res = await _dataSource.Set(course, nameof(Courses));
            return res;
        }
    }
}
