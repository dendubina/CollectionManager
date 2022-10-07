using System;

namespace Contracts
{
    public interface ILikesRepository
    {
        void PutLike(Guid itemId, Guid authorId);

        void RemoveLike(Guid itemId, Guid authorId);
    }
}
