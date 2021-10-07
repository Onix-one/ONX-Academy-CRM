using ONX.CRM.WebAPI.Enums;

namespace ONX.CRM.WebAPI.Dto
{
    public struct StudentsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public int? GroupId { get; set; }
        public string GroupNumber { get; set; }
        public int? CourseId { get; set; }
        public string CourseTitle { get; set; }
        public StudentType? Type { get; set; }
    }
}
