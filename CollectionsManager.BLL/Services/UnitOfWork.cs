using AutoMapper;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CollectionsManager.BLL.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        private ICollectionsService _collectionsService;
        private ICommentsService _commentsService;
        private IItemsService _itemsService;
        private ILikesService _likesService;
        private ITagsService _tagsService;

        public UnitOfWork(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public ICollectionsService Collections
        {
            get
            {
                if (_collectionsService is null)
                {
                    _collectionsService = new CollectionsService(_repositoryManager, _mapper, _userManager);
                }

                return _collectionsService;
            }
        }

        public ICommentsService Comments
        {
            get
            {
                if (_commentsService is null)
                {
                    _commentsService = new CommentsService(_repositoryManager, _mapper);
                }

                return _commentsService;
            }
        }

        public IItemsService Items
        {
            get
            {
                if (_itemsService is null)
                {
                    _itemsService = new ItemsService(_repositoryManager, _mapper, _userManager);
                }

                return _itemsService;
            }
        }

        public ILikesService Likes
        {
            get
            {
                if (_likesService is null)
                {
                    _likesService = new LikesService(_repositoryManager);
                }

                return _likesService;
            }
        }

        public ITagsService Tags
        {
            get
            {
                if (_tagsService is null)
                {
                    _tagsService = new TagsService(_repositoryManager);
                }

                return _tagsService;
            }
        }
    }
}
