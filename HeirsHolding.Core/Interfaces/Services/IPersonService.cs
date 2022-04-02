using HeirsHolding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Core.Interfaces.Services
{
    public interface IPersonService
    {
        Task<List<Persons>> UploadPersons(List<Persons> person);
        Task<List<Persons>> GetAllPersons();
        Task<Persons> GetPersonById(string personId);
        Task<List<Courses>> GetPersonCourses(string personId);
        Task<double> GetPersonAverageScore(string personId);
    }
}
