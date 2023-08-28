
namespace Codev.JobApplication.Features.Persistence.Applicant
{
    public interface IApplicantQueryService
    {
        Task<Applicant> FilterApplicantByEmailAsync(string emailAddress);
    }
}
