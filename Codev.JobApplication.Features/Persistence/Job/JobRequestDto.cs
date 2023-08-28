namespace Codev.JobApplication.Features.Persistence.Job
{
    public class JobRequestDto
    {
        public Guid ? Id { get; set; }
        public int? NoOfOpenings { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public JobIndustryType? Industry { get; set; }
    }
}
