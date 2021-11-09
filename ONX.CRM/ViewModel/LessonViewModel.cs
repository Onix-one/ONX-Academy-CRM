using System;
using System.ComponentModel.DataAnnotations;

namespace ONX.CRM.ViewModel
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field cannot be empty")]
        public string Number { get; set; }
        [Required(ErrorMessage = "This field cannot be empty")]
        public string Topic { get; set; }
        public string? Description { get; set; }
        public string? Homework { get; set; }
        [Required(ErrorMessage = "This field cannot be empty")]
        public DateTime? Date { get; set; }
        public int? GroupId { get; set; }
        public byte[]? Materials { get; set; }
    }
}
