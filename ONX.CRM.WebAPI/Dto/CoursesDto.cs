namespace ONX.CRM.WebAPI.Dto
{
    public struct CoursesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NecessaryPreKnowledge { get; set; }
        public decimal Cost { get; set; }
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
    }
}
