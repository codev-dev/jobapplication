using Codev.JobApplication.DataRepository;
using Codev.JobApplication.Features.Persistence.Job;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace Codev.JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IDataRepository<Job> dataRepository;
        private readonly IJobQueryService jobQueryService;
        public JobController(IDataRepository<Job> dataRepository, IJobQueryService jobQueryService)
        {
            this.dataRepository = dataRepository;
            this.jobQueryService = jobQueryService;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterAsync(string ? keyword, JobIndustryType ? jobIndustryType)
        {
            var data = await jobQueryService.FilterJobsAsync(keyword, jobIndustryType);
            return Ok(data);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await dataRepository.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await dataRepository.GetAllAsync();
            return Ok(data);
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertAsync(JobRequestDto job)
        {
            var jobRequest = job.Adapt<Job>();
            Guid ? id = await dataRepository.AddAsync(jobRequest);
            return Ok(id);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(JobRequestDto job)
        {
            var jobRequest = job.Adapt<Job>();
            bool success = await dataRepository.UpdateAsync(jobRequest);
            return Ok(success);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await dataRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
