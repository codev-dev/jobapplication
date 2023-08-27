using Codev.JobApplication.Dapper;
using Codev.JobApplication.DataRepository;


namespace Codev.JobApplication.Features.Persistence.Job
{
    public class JobDataService : IDataRepository<Job>
    {
        private readonly IDapperBase dapperBase;
        public JobDataService(IDapperBase dapperBase)
        {
            this.dapperBase = dapperBase;
        }

        public async Task AddAsync(Job entity)
        {
           await dapperBase.InsertAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var data = await GetByIdAsync(id);
            await dapperBase.DeleteAsync(data);
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            string query = "SELECT * FROM Job";
            return await dapperBase.QueryListAsync<Job>(query);
        }

        public async Task<Job> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM Job WHERE id = @id";
            return await dapperBase.QueryFirstOrDefaultAsync<Job>(query, new { id = id });
        }

        public async Task UpdateAsync(Job entity)
        {
            await dapperBase.UpdateAsync(entity);
        }
    }
}
