using System.Threading.Tasks;

namespace CollectionsManager.DAL.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        ICollectionRepository Collections { get; }

        IItemsRepository Items { get; }

        ICommentsRepository Comments { get; }

        ILikesRepository Likes { get; }

        ITagsRepository Tags { get; }

        ICustomFieldValuesRepository CustomFieldValues { get; }

        Task SaveAsync();
    }
}
