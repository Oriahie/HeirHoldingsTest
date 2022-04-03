using HeirsHolding.Core.Entities;
using HeirsHolding.Core.Interfaces.Database;
using HeirsHolding.Infrastructure.Services;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeirsHolding.Tests.Services
{
    public class CourseServiceTest
    {
        private readonly Mock<IDataSource> _dataSource;
        private readonly CourseService _courseService;
        private readonly List<Courses> _courses;
        private readonly Courses _courseOne;
        private readonly Courses _courseTwo;
        public CourseServiceTest()
        {
            _dataSource = new Mock<IDataSource>();
            _courseService = new CourseService(_dataSource.Object);
            _courseOne = new Courses
            {
                Id = "c1",
                Name = "Accounting 101"
            };
            _courseTwo = new Courses
            {
                Id = "c2",
                Name = "Biology 101"
            };

            _courses = new List<Courses>(){_courseOne,_courseTwo};
        }

        #region GetAllCourses

        [Test]
        public async Task GetAllCourses_WhenNoCourseFound_ReturnsEmpty()
        {
            _dataSource.Setup(x => x.Get<Courses>(nameof(Courses)))
                       .ReturnsAsync(new List<Courses>());
            var res = await _courseService.GetAllCourses();

            res.ShouldBeEmpty();
        }


        [Test]
        public async Task GetAllCourses_WhenCoursesAvailable_ReturnsListOfCourses()
        {
            _dataSource.Setup(x => x.Get<Courses>(nameof(Courses)))
                       .ReturnsAsync(_courses);
            var res = await _courseService.GetAllCourses();

            res.ShouldBe(_courses);
        }

        #endregion

        #region GetCourseById
        [Test]
        public async Task GetCourseById_WhenCourseNotFound_ReturnsException()
        {
            _dataSource.Setup(x => x.Get<Courses>(nameof(Courses)))
                       .Throws<Exception>();
            await Should.ThrowAsync<Exception>(async () => await _courseService.GetCourseById("c1"));
        }

        [Test]
        public async Task GetCourseById_WhenCourseFound_ReturnsCourse()
        {
            _dataSource.Setup(x => x.Get<Courses>(nameof(Courses)))
                       .ReturnsAsync(_courses);
            var res = await _courseService.GetCourseById("c1");

            res.ShouldBe(_courseOne);
        }

        #endregion


        #region UploadCourses
        [Test]
        public async Task UploadCourses_WhenCalled_ReturnsUploadedCourses()
        {
            _dataSource.Setup(x => x.Set(_courses,nameof(Courses)))
                       .ReturnsAsync(_courses);
            var res = await _courseService.UploadCourses(_courses);

            res.ShouldBe(_courses);
        }

        #endregion
    }
}
