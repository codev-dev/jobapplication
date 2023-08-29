

using Codev.JobApplication.Dapper;
using Codev.JobApplication.DataRepository;
using Codev.JobApplication.Features.Persistence.Applicant;

namespace Codev.JobApplication.Features.Persistence.JobApplication
{
    public class JobApplicationDataService : IJobApplicationManager, IApplicantQueryService
    {
        private readonly IDataRepository<Codev.JobApplication.Features.Persistence.Applicant.Applicant> applicantDataService;
        private readonly IDataRepository<Codev.JobApplication.Features.Persistence.Job.Job> jobDataService;
        private readonly IDapperBase dapperBase;
        public JobApplicationDataService(IDapperBase dapperBase, 
            IDataRepository<Codev.JobApplication.Features.Persistence.Applicant.Applicant> applicantDataService,
            IDataRepository<Codev.JobApplication.Features.Persistence.Job.Job> jobDataService)
        {
            this.dapperBase = dapperBase;
            this.applicantDataService = applicantDataService;
            this.jobDataService = jobDataService;
        }

        public async Task<bool> ApplicantAppliedForJobByIdAsync(Guid jobId, Guid applicantId)
        {
            string query = "SELECT COUNT(1) FROM JobApplication WHERE JobId = @JobId AND ApplicantId = @ApplicantId";
            int count = await dapperBase.QueryFirstOrDefaultAsync<int>(query, new { JobId = jobId, ApplicantId = applicantId });
            return count > 0;
        }

        public async Task<Guid> ApplyJobAsync(Guid jobId, Guid applicantId)
        {
            var job = await jobDataService.GetByIdAsync(jobId);
            var applicant = await applicantDataService.GetByIdAsync(applicantId);

            if (job is null) throw new Exception("Job does not exist");
            if (applicant is null) throw new Exception("Applicant does not exist.");
            
            Guid id = Guid.NewGuid();

            return await dapperBase.InsertAsync(new JobApplication()
            {
                Id = id,
                JobId = jobId,
                ApplicantId = applicantId
            });
        }

        public async Task<Applicant.Applicant> FilterApplicantByEmailAsync(string emailAddress)
        {
            string query = "SELECT * FROM Applicant WHERE EmailAddress = @EmailAddress";
            return await dapperBase.QueryFirstOrDefaultAsync<Applicant.Applicant>(query, new { EmailAddress = emailAddress });
        }

        public async Task<IEnumerable<Job.Job>> GetAllAppliedJobsByApplicantIdAsync(Guid applicantId)
        {
            string query = "  SELECT j.* FROM Job j " +
                " INNER JOIN JobApplication ja " +
                " ON ja.JobId = j.Id" +
                " WHERE ja.ApplicantId = @ApplicantId";

            return await dapperBase.QueryListAsync<Job.Job>(query, new { ApplicantId = applicantId });
        }
    }
}
