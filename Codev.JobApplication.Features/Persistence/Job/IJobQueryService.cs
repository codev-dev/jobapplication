namespace Codev.JobApplication.Features.Persistence.Job
{
    public interface IJobQueryService
    {
        Task<IEnumerable<Job>> FilterJobsAsync(string ? keyword, JobIndustryType ? jobIndustryType);
    }
}
