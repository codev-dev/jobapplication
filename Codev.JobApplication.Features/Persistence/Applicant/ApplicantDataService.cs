using Codev.JobApplication.Dapper;
using Codev.JobApplication.DataRepository;

namespace Codev.JobApplication.Features.Persistence.Applicant
{
    public class ApplicantDataService : IDataRepository<Applicant>
    {
        private readonly IDapperBase dapperBase;
        public ApplicantDataService(IDapperBase dapperBase)
        {
            this.dapperBase = dapperBase;
        }
        public async Task<IEnumerable<Applicant>> GetAllAsync()
        {
            string query = "SELECT * FROM Applicant";
            return await dapperBase.QueryListAsync<Applicant>(query);
        }

        public async Task<Applicant> GetByIdAsync(Guid id)
        {
            string query = "SELECT * FROM Applicant WHERE Id = @Id";
            return await dapperBase.QueryFirstOrDefaultAsync<Applicant>(query, new { Id = id });
        }
        public async Task<Guid> AddAsync(Applicant entity)
        {
            entity.Id = Guid.NewGuid();
            return await dapperBase.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            string deleteQuery = "DELETE FROM Applicant WHERE Id = @Id";

            int count = await dapperBase.ExecuteNonQueryAsync(deleteQuery, new { Id = id });
            return count > 0;
        }

        public async Task<bool> UpdateAsync(Applicant entity)
        {
            string updateQuery = "UPDATE Applicant SET FullName = @FullName, EmailAddress = @EmailAddress " +
                  " WHERE Id = @Id";
            int count = await dapperBase.ExecuteNonQueryAsync(updateQuery, entity);
            return count > 0;
        }
    }
}
