using System.Threading.Tasks;

namespace LikeButton.Domain.IRepositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
