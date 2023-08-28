namespace Codev.JobApplication.Features.Persistence.Applicant
{
    public class ApplicantRequestDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }
}
