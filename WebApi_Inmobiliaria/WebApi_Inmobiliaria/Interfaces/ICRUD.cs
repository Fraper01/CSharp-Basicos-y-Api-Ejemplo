namespace WebApi_Clinica.Interfaces
{
    public interface ICRUD<T>
    {
        Task<T?> CreateAsync(T Entity);
        Task<List<T>?> GetAllAsync();
        Task<List<T>?> ReadAllAsync();

        Task<T?> GetAsync(object id);
        Task<T?> UpdateAsync(T Entity);
        Task<T?> DeleteAsync(object id);
        Task<T?> GetByIdAsync(int id);
        Task<T?> AddAsync(T Entity);
        Task<bool> ExistsAsync(int id);
    }
}
