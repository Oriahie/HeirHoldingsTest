using HeirsHolding.Core.Entities;
using HeirsHolding.Core.Interfaces.Database;
using HeirsHolding.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Infrastructure.Services
{
    public class PersonService : IPersonService
    {
        private readonly IDataSource _dataSource;
        public PersonService(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public async Task<List<Persons>> GetAllPersons()
        {
            var res = await _dataSource.Get<Persons>(nameof(Persons));
            return res;
        }
               
        public async Task<double> GetPersonAverageScore(string personId)
        {
            var persons = await _dataSource.Get<Persons>(nameof(Persons));
            var person = persons.Where(x => x.Person_Id == personId).ToList();
            if (person == default) throw new Exception("Person Not Found");
            var avgScore = person.Average(x => x.Score);
            return avgScore;
        }
               
        public async Task<Persons> GetPersonById(string personId)
        {
            var persons = await _dataSource.Get<Persons>(nameof(Persons));
            var person = persons.FirstOrDefault(x => x.Person_Id == personId);
            if (person == null) throw new Exception("Person Not Found");
            return person;
        }
               
        public async Task<List<Courses>> GetPersonCourses(string personId)
        {
            var persons = await _dataSource.Get<Persons>(nameof(Persons));
            var person = persons.Where(x => x.Person_Id == personId).ToList();
            if (person == default) throw new Exception("Person Not Found");
            var coursesPersonTakes = person.Select(c => c.Course_Id).ToList();


            var courses = await _dataSource.Get<Courses>(nameof(Courses));
            var res = courses.Where(x => coursesPersonTakes.Contains(x.Id)).ToList();
            return res;
        }
               
        public async Task<List<Persons>> UploadPersons(List<Persons> person)
        {
            var res = await _dataSource.Set(person, nameof(Persons));
            return res;
        }
    }
}
