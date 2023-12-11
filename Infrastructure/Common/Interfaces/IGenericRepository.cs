namespace Infrastructure.Common.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task Create(TEntity item);
        public Task<TEntity> CreateAndReturn(TEntity item);
        public Task CreateRange(List<TEntity> listItems);
        public Task<TEntity?> FindById(int id);
        public IQueryable<TEntity> Get();
        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        public Task Remove(int id);
        public Task Remove(TEntity item);
        public Task RemoveRange(IEnumerable<TEntity> items);
        public Task Update(TEntity item);
        public Task<TEntity> UpdateAndReturn(TEntity item);
    }
}
