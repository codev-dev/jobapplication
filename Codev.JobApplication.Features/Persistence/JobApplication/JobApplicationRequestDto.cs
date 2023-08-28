namespace Codev.JobApplication.Features.Persistence.JobApplication
{
    public class JobApplicationRequestDto
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public Guid ApplicantId { get; set; }
    }
}
