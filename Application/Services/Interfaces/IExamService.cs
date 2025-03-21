using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.ExamDTO;

namespace StudentManagement.Application.Services.Interfaces
{
    public interface IExamService
    {
        ExamSubmissionResultDTO SubmitExam(SubmitExamDTO request);
    }
}