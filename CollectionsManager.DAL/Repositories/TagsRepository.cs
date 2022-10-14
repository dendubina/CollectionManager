using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionsManager.DAL.EF;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.DAL.Repositories
{
    public class TagsRepository : RepositoryBase<Tag>, ITagsRepository
    {
        public TagsRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IQueryable<Tag> GetAll(bool trackChanges)
            => FindAll(trackChanges);

        public async Task<IEnumerable<Tag>> CreateTags(IEnumerable<Tag> tags)
        {
            var enumerable = tags as Tag[] ?? tags.ToArray();
            await DbContext.Tags.AddRangeAsync(enumerable.Except(await FindAll(trackChanges: false).ToArrayAsync()).ToList());
            await DbContext.SaveChangesAsync();

            return FindAll(trackChanges: true)
                .Where(x => enumerable.Select(n => n.Name).Any(f => f == x.Name))
                .ToList();
        }
    }
}
