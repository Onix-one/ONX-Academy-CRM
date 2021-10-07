using System;
using System.ComponentModel.DataAnnotations;
using ONX.CRM.BLL.Enums;

namespace ONX.CRM.ViewModel
{
    public class StudentRequestViewModel : PersonViewModel
    {
        public DateTime? Created { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public int CourseId { get; set; }
        public CourseViewModel Course { get; set; }
        public StudentType? Type { get; set; }
        public string? Comments { get; set; }
        public bool Selected { get; set; }
    }
}
