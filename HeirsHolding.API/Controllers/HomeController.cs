using HeirsHolding.Core.Entities;
using HeirsHolding.Core.Interfaces.Services;
using HeirsHolding.Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeirsHolding.API.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IPersonService _personService;
        public HomeController(ICourseService courseService,
                              IPersonService personService)
        {
            _courseService = courseService;
            _personService = personService;
        }

        [HttpPost("uploadcourses")]
        public async Task<IActionResult> UploadCourses([FromBody] CourseDTO model)
        {
            try
            {
                #region validation
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    var message = string.Join(',', errors);
                    throw new Exception(message);
                }
                #endregion


                var res =await _courseService.UploadCourses(model.Courses);
                return Ok(new Response<List<Courses>>
                {
                    Data = res,
                    Succeeded = true,
                    Message = "Retrieved Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>
                {
                    Succeeded = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("uploadpersons")]
        public async Task<IActionResult> UploadPersons([FromBody] PersonDTO model)
        {
            try
            {
                #region validation
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    var message = string.Join(',', errors);
                    throw new Exception(message);
                }
                #endregion

                var res = await _personService.UploadPersons(model.Persons);
                return Ok(new Response<List<Persons>>
                {
                    Data = res,
                    Succeeded = true,
                    Message = "Retrieved Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>
                {
                    Succeeded = false,
                    Message = ex.Message
                });
            }
        
        }
    
        [HttpGet("personalprogress/{personId}")]
        public async Task<IActionResult> PersonalProgress([Required]string personId)
        {
            try
            {

                var res = new PersonalProgressDTO
                {
                    PersonId = personId,
                    Courses = await _personService.GetPersonCourses(personId),
                    AverageScore = await _personService.GetPersonAverageScore(personId),
                    Name = (await _personService.GetPersonById(personId)).Name
                };
                return Ok(new Response<PersonalProgressDTO>
                {
                    Data = res,
                    Succeeded = true,
                    Message = "Retrieved Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>
                {
                    Succeeded = false,
                    Message = ex.Message
                });
            }

        }

    }
}
