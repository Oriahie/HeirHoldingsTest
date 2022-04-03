using HeirsHolding.Core.Entities;
using HeirsHolding.Core.Interfaces.Database;
using HeirsHolding.Infrastructure.Services;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Tests.Services
{
    public class PersonServiceTest
    {
        private readonly Mock<IDataSource> _dataSource;
        private readonly PersonService _personService;
        private readonly List<Persons> _persons;
        private readonly List<Courses> _courses;
        private readonly Persons _person;

        public PersonServiceTest()
        {
            _dataSource = new Mock<IDataSource>();
            _personService = new PersonService(_dataSource.Object);


            _courses = new List<Courses>() { 
                new Courses
            {
                Id = "c1",
                Name = "Accounting 101"
            }, 
                new Courses
            {
                Id = "c2",
                Name = "Biology 101"
            } };

            _person = new Persons
            {
                Person_Id = "e123",
                Course_Id = "c1",
                Name = "John Doe",
                Score = 5
            };
            _persons = new List<Persons>()
            {
                _person,
                new Persons
                {
                    Person_Id = "e124",
                    Course_Id = "c2",
                    Name = "Jane Doe",
                    Score = 5
                },
                new Persons
                {
                    Person_Id = "e123",
                    Course_Id = "c2",
                    Name = "John Doe",
                    Score = 4
                }
            };
        }


        #region GetAllPersons
        [Test]
        public async Task GetAllPersons_WhenNoPersonsFound_ReturnsEmpty()
        {
            _dataSource.Setup(x => x.Get<Persons>(nameof(Persons)))
                       .ReturnsAsync(new List<Persons>());
            var res = await _personService.GetAllPersons();

            res.ShouldBeEmpty();
        }


        [Test]
        public async Task GetAllPersons_WhenPersonsFound_ReturnsPersons()
        {
            _dataSource.Setup(x => x.Get<Persons>(nameof(Persons)))
                       .ReturnsAsync(_persons);
            var res = await _personService.GetAllPersons();

            res.ShouldBe(_persons);
        }
        #endregion


        #region GetPersonById
        [Test]
        public async Task GetPersonById_WhenNotFound_ReturnsException()
        {
            _dataSource.Setup(x => x.Get<Persons>(nameof(Persons)))
                       .Throws<Exception>();
            await Should.ThrowAsync<Exception>(async () => await _personService.GetPersonById("e123"));
        }

        [Test]
        public async Task GetCourseById_WhenCourseFound_ReturnsCourse()
        {
            _dataSource.Setup(x => x.Get<Persons>(nameof(Persons)))
                       .ReturnsAsync(_persons);
            var res = await _personService.GetPersonById(_person.Person_Id);

            res.ShouldBe(_person);
        }

        #endregion

        #region GetPersonAverageScore
        [Test]
        public async Task GetPersonAverageScore_WhenPersonNotFound_ReturnsException()
        {
            _dataSource.Setup(x => x.Get<Persons>(nameof(Persons)))
                       .ReturnsAsync(_persons);
            await Should.ThrowAsync<Exception>(async () => await _personService.GetPersonAverageScore("e129"));
        }

        [Test]
        public async Task GetPersonAverageScore_WhenCalled_ReturnsAverageScore()
        {
            _dataSource.Setup(x => x.Get<Persons>(nameof(Persons)))
                       .ReturnsAsync(_persons);
            var res = await _personService.GetPersonAverageScore(_person.Person_Id);

            res.ShouldBe(4.5);
        }
        #endregion


        #region GetPersonCourses
        [Test]
        public async Task GetPersonCourses_WhenPersonNotFound_ReturnsException()
        {
            _dataSource.Setup(x => x.Get<Persons>(nameof(Persons)))
                       .ReturnsAsync(_persons);
            await Should.ThrowAsync<Exception>(async () => await _personService.GetPersonCourses("e129"));
        }

        [Test]
        public async Task GetPersonCourses_WhenCalled_ReturnsListOfCourses()
        {
            _dataSource.Setup(x => x.Get<Persons>(nameof(Persons))).ReturnsAsync(_persons);
            _dataSource.Setup(x => x.Get<Courses>(nameof(Courses))).ReturnsAsync(_courses);

            var res = await _personService.GetPersonCourses("e124");

            res.ShouldAllBe(x=>x.Id == "c2");
        }

        #endregion

        #region UploadPersons
        [Test]
        public async Task UploadPersons_WhenCalled_ReturnsUploadedCourses()
        {
            _dataSource.Setup(x => x.Set(_persons, nameof(Persons)))
                       .ReturnsAsync(_persons);
            var res = await _personService.UploadPersons(_persons);

            res.ShouldBe(_persons);
        }
        #endregion
    }
}
