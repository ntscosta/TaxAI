namespace TaxAI.Domain.Interfaces
{
    public interface IRepository<TEntity, TKey> : IDisposable
        where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task AddRange(List<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity?> GetById(TKey id);

        IRepositoryUow UnitOfWork { get; }
    }
}
