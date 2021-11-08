namespace ONX.CRM.Configuration
{
    public class SecurityOptions
    {
        public const string SectionTitle = "Security";
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerPassword { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherPassword { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPassword { get; set; }
    }
}
