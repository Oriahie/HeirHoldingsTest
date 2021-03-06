using HeirsHolding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Core.Interfaces.Services
{
    public interface ICourseService
    {
        Task<List<Courses>> UploadCourses(List<Courses> course);
        Task<List<Courses>> GetAllCourses();
        Task<Courses> GetCourseById(string courseId);
    }
}
