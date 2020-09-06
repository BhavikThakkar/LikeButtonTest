using System.Threading.Tasks;
using LikeButton.Domain.IRepositories;

namespace LikeButton.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
