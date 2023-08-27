using Codev.JobApplication.Common.Persistence;

namespace Codev.JobApplication.Features.Persistence.Job
{
    public class JobDataService : IRepository
    {
        public JobDataService()
        {
            
        }
        public Task<T> GetAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetManyAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync<T>(T data)
        {
            throw new NotImplementedException();
        }

        public Task Update<T>(int id, T data)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsycn(int id)
        {
            throw new NotImplementedException();
        }
    }
}
