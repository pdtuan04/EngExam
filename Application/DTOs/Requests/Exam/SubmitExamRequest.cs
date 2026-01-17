using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Requests.Question;
using Domain.Entity;

namespace Application.DTOs.Requests.Exam
{
    public class SubmitExamRequest
    {
        [Required]
        public Guid ExamId { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; } = [];
    }
}
