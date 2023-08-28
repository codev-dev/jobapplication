namespace Codev.JobApplication.Features.Persistence.JobApplication
{
    public interface IJobApplicationManager
    {
        Task<Guid> ApplyJobAsync(Guid jobId, Guid applicantId);
        Task<bool> ApplicantAppliedForJobByIdAsync(Guid jobId, Guid applicantId);
        Task<IEnumerable<Codev.JobApplication.Features.Persistence.Job.Job>> GetAllAppliedJobsByApplicantIdAsync(Guid applicantId);
    }
}
