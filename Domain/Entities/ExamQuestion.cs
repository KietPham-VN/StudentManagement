﻿namespace StudentManagement.Domain.Entities;

public class ExamQuestion
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public int QuestionId { get; set; }

    public Exam Exam { get; set; }
    public Question Question { get; set; }
}