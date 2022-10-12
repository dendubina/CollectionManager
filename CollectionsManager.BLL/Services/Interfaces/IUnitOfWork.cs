namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface IUnitOfWork
    {
        ICollectionsService Collections { get; }

        ICommentsService Comments { get; }

        IItemsService Items { get; }

        ILikesService Likes { get; }

        ITagsService Tags { get; }
    }
}
