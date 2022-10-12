using System;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.DAL.Repositories.Interfaces
{
    public interface ILikesRepository
    {
        void PutLike(Like like);

        void RemoveLike(string authorId, Guid itemId);
    }
}
