using System.Threading.Tasks;
using Contracts;
using Entities.EF;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICollectionRepository _collectionRepository;
        private IItemsRepository _itemsRepository;
        private ICommentsRepository _commentsRepository;
        private ILikesRepository _likesRepository;
        private ITagsRepository _tagsRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICollectionRepository Collections
        {
            get
            {
                if (_collectionRepository is null)
                {
                    _collectionRepository = new CollectionsRepository(_context);
                }

                return _collectionRepository;
            }
        }

        public IItemsRepository Items
        {
            get
            {
                if (_itemsRepository is null)
                {
                    _itemsRepository = new ItemsRepository(_context);
                }

                return _itemsRepository;
            }
        }

        public ICommentsRepository Comments
        {
            get
            {
                if (_commentsRepository is null)
                {
                    _commentsRepository = new CommentsRepository(_context);
                }

                return _commentsRepository;
            }
        }

        public ILikesRepository Likes
        {
            get
            {
                if (_likesRepository is null)
                {
                    _likesRepository = new LikesRepository(_context);
                }

                return _likesRepository;
            }
        }

        public ITagsRepository Tags
        {
            get
            {
                if (_tagsRepository is null)
                {
                    _tagsRepository = new TagsRepository(_context);
                }

                return _tagsRepository;
            }
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
