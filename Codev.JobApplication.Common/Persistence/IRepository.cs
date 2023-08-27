namespace Codev.JobApplication.Common.Persistence
{
    public interface IRepository
    {
        Task<T> GetAsync<T>(int id);
        Task<IEnumerable<T>> GetManyAsync<T>();
        Task InsertAsync<T>(T data);
        Task Update<T>(int id, T data);
        Task DeleteAsycn(int id);
    }
}
