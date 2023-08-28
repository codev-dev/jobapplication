using Codev.JobApplication.DataRepository;
using Codev.JobApplication.Features.Persistence.Applicant;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Codev.JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IDataRepository<Applicant> dataRepository;
        public ApplicantController(IDataRepository<Applicant> dataRepository)
        {
            this.dataRepository = dataRepository;
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
        public async Task<IActionResult> InsertAsync(ApplicantRequestDto applicant)
        {
            var applicantRequest = applicant.Adapt<Applicant>();
            Guid? id = await dataRepository.AddAsync(applicantRequest);
            return Ok(id);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(ApplicantRequestDto applicant)
        {
            var applicantRequest = applicant.Adapt<Applicant>();
            bool success = await dataRepository.UpdateAsync(applicantRequest);
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
