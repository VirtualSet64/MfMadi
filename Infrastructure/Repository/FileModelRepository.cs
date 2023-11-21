using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;

namespace Infrastructure.Repository
{
    public class FileModelRepository : GenericRepository<FileModel>, IFileModelRepository
    {
        public FileModelRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
    }
}
