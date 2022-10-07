using System.Threading.Tasks;

namespace Contracts
{
    public interface IUnitOfWork
    {
        ICollectionRepository Collections { get; }

        IItemsRepository Items { get; }

        ICommentsRepository Comments { get; }

        ILikesRepository Likes { get; }

        Task SaveAsync();
    }
}
