using Codev.JobApplication.DataRepository;
using Codev.JobApplication.Features.Persistence.Applicant;
using Codev.JobApplication.Features.Persistence.JobApplication;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Codev.JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicantController : ControllerBase
    {
        private readonly IDataRepository<Applicant> applicantDataRepository;
        private readonly IApplicantQueryService applicantQueryService;
        private readonly IJobApplicationManager jobApplicationManager;
        public JobApplicantController(IJobApplicationManager jobApplicationManager, 
            IDataRepository<Applicant> applicantDataRepository, 
            IApplicantQueryService applicantQueryService)
        {
            this.jobApplicationManager = jobApplicationManager;
            this.applicantDataRepository = applicantDataRepository;
            this.applicantQueryService = applicantQueryService;
        }

        [HttpPost("applyjob/{jobId}")]
        public async Task<IActionResult> ApplyJobAsync(ApplicantRequestDto applicant, Guid jobId)
        {
            Guid applicantId;
            var applicantEntity = applicant.Adapt<Applicant>();
            if (string.IsNullOrEmpty(applicantEntity.EmailAddress) || string.IsNullOrEmpty(applicantEntity.FullName)) 
            {
                return BadRequest("Fullname and Emailaddress are required.");
            }

            var applicantData = await applicantQueryService.FilterApplicantByEmailAsync(applicantEntity.EmailAddress);
            if (applicantData is null)
            {
                applicantId = await applicantDataRepository.AddAsync(applicantEntity);
            }
            else 
            {
                applicantId = applicantData.Id;
            }
            
            var appliedJob = await jobApplicationManager.ApplicantAppliedForJobByIdAsync(jobId, applicantId);
            if (!appliedJob) 
            {
                await jobApplicationManager.ApplyJobAsync(jobId, applicantId);
                return Ok(true);
            }
            return BadRequest("You have previously applied for this job.");
        }

        [HttpGet("getjobsapplied/{id}")]
        public async Task<IActionResult> GetAllAppliedJobsByApplicantIdAsync(Guid id)
        {
            var data = await jobApplicationManager.GetAllAppliedJobsByApplicantIdAsync(id);
            return Ok(data);
        }
    }
}
