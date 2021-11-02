﻿namespace ONX.CRM.DAL.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? ImgLink { get; set; }
        public string? UserId { get; set; }
    }
}