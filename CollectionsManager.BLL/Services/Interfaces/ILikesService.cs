using System;
using System.Threading.Tasks;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface ILikesService
    {
        Task PutLikeAsync(Guid itemId, string authorId);

        Task RemoveLikeAsync(Guid itemId, string authorId);
    }
}
