using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.ExamDTO;
using StudentManagement.Application.DTOs.SchoolDTO;
using StudentManagement.Application.Services.Implementation;
using StudentManagement.Application.Services.Interfaces;

namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("api/exam")]
    public class ExamController(IExamService _examService) : ControllerBase
    {
        //[HttpPost]
        //public IActionResult CreateExam([FromBody] CreateExamDTO request)
        //{
        //    return _examService.CreateExam(CreateExamDTO request);
        //}

        //[HttpPut]
        //public IActionResult UpdateExam()
        //{
        //}

        //[HttpDelete]
        //public IActionResult DeleteExam()
        //{
        //}

        [HttpPost("submit")]
        public IActionResult SubmitExam([FromBody] SubmitExamDTO request)
        {
            return Ok(_examService.SubmitExam(request));
        }
    }
}