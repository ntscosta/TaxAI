namespace TaxAI.Domain.Interfaces
{
    public interface IRepositoryUow
    {
        Task<bool> SaveAsync();
        Task<bool> BulkSalve();
    }
}