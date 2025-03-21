using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.ExamDTO;
using StudentManagement.Application.Services.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructures;

namespace StudentManagement.Application.Services.Implementation
{
    public class ExamService(IApplicationDbContext _context) : IExamService
    {
        public ExamSubmissionResultDTO SubmitExam(SubmitExamDTO request)
        {
            var exam = _context.Exams
                               .Include(e => e.ExamQuestions)
                               .ThenInclude(eq => eq.Question)
                               .FirstOrDefault(e => e.Id == request.ExamId) ?? throw new KeyNotFoundException("Exam not found.");
            var student = _context.Students.Find(request.StudentId) ?? throw new KeyNotFoundException("Student not found.");
            int correctCount = 0;
            int totalQuestions = exam.ExamQuestions.Count;

            var submission = new ExamSubmission
            {
                ExamId = request.ExamId,
                StudentId = request.StudentId,
                SubmittedAt = DateTime.UtcNow
            };

            _context.ExamSubmissions.Add(submission);
            _context.SaveChanges();

            List<StudentAnswer> studentAnswers = [];

            foreach (var question in exam.ExamQuestions)
            {
                bool isCorrect = request.Answers.ContainsKey(question.QuestionId) &&
                                 request.Answers[question.QuestionId] == question.Question.CorrectAnswer;

                if (isCorrect) correctCount++;

                studentAnswers.Add(new StudentAnswer
                {
                    ExamSubmissionId = submission.Id,
                    QuestionId = question.QuestionId,
                    SelectedAnswer = request.Answers.TryGetValue(question.QuestionId, out char value)
                                     ? value : ' ',
                    IsCorrect = isCorrect
                });
            }

            _context.StudentAnswers.AddRange(studentAnswers);

            submission.Score = (totalQuestions > 0) ? 10f * correctCount / totalQuestions : 0f;
            _context.SaveChanges();

            return new ExamSubmissionResultDTO
            {
                ExamSubmissionId = submission.Id,
                Score = submission.Score,
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctCount,
                Message = "Exam submitted successfully."
            };
        }
    }
}