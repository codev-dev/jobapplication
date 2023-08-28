using Codev.JobApplication.Dapper;
using Codev.JobApplication.DataRepository;
using static Dapper.SqlMapper;
using static Slapper.AutoMapper;


namespace Codev.JobApplication.Features.Persistence.Job
{
    public class JobDataService : IDataRepository<Job>, IJobQueryService
    {
        private readonly IDapperBase dapperBase;
        public JobDataService(IDapperBase dapperBase)
        {
            this.dapperBase = dapperBase;
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            string query = "SELECT * FROM Job";
            return await dapperBase.QueryListAsync<Job>(query);
        }

        public async Task<Job> GetByIdAsync(Guid id)
        {
            string query = "SELECT * FROM Job WHERE id = @id";
            return await dapperBase.QueryFirstOrDefaultAsync<Job>(query, new { id = id });
        }

        public async Task<IEnumerable<Job>> FilterJobsAsync(string? keyword, JobIndustryType? jobIndustryType)
        {
            string query = "SELECT [Id], [NoOfOpenings], LOWER([Title]) AS Title, LOWER([Description]) AS Description, [Industry] FROM Job WHERE 1 = 1";
            if (!string.IsNullOrEmpty(keyword))
            {
                query += " AND (Title LIKE @Keyword OR Description LIKE @Keyword)";
            }
            if (jobIndustryType is not null && (int)jobIndustryType > 0)
            {
                query += " AND (industry = @IndustryId)";
            }
            return await dapperBase.QueryAsListAsync<Job>(query, new { Keyword = "%" + keyword + "%", IndustryId = jobIndustryType });
        }
        public async Task<Guid> AddAsync(Job entity)
        {
            entity.Id = Guid.NewGuid();
            return await dapperBase.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            string deleteQuery = "DELETE FROM Job WHERE Id = @Id";

            int count = await dapperBase.ExecuteNonQueryAsync(deleteQuery, new { Id = id });
            return count > 0;
        }
        public async Task<bool> UpdateAsync(Job entity)
        {
            string updateQuery = "UPDATE Job SET NoOfOpenings = @NoOfOpenings, Title = @Title, Description = @Description, Industry = @Industry" +
                " WHERE Id = @Id";
            int count = await dapperBase.ExecuteNonQueryAsync(updateQuery, entity);
            return count > 0;
        }
    }
}
