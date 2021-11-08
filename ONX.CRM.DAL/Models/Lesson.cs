using System;

namespace ONX.CRM.DAL.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Topic { get; set; }
        public string? Description { get; set; }
        public string? Homework { get; set; }
        public DateTime? Date { get; set; }
        public int GroupId { get; set; }
        public byte[]? Materials { get; set; }
    }
}
