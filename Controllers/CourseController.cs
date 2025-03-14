using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.CourseDTO;
using StudentManagement.Application.Services.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Controllers.Implementations
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController(ICourseService courseService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<CourseViewModel>), 200)]
        public IActionResult GetCourses()
        {
            var courses = courseService.GetCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CourseViewModel), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetCourse(int id)
        {
            var course = courseService.GetCourseById(id);
            if (course == null)
            {
                return NotFound(new { message = $"No Course Found With ID: {id}" });
            }
            return Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Course), 201)]
        [ProducesResponseType(400)]
        public IActionResult CreateCourse([FromBody] CourseCreateModel courseCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCourse = courseService.CreateCourse(courseCreateModel);
            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.Id }, createdCourse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCourse(int id, [FromBody] CourseUpdateModel courseUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = courseService.EditCourse(courseUpdateModel, id);
            return course != null ? Ok("Update Successfully") : NotFound(new { message = $"No Course Found With ID: {id}" });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCourse(int id)
        {
            var Course = courseService.DeleteCourse(id);
            return Course != null ? Ok("Delete Successfully") : NotFound(new { message = $"No Course Found With ID: {id}" });
        }
    }
}