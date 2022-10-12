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
            await DbContext.Tags.AddRangeAsync(enumerable.Except(await GetAll(trackChanges: false).ToArrayAsync()));
            await DbContext.SaveChangesAsync();

            return FindAll(trackChanges: false)
                .Where(x => enumerable.Select(n => n.Name).Any(f => f == x.Name))
                .ToList();
        }
       

        /* public async Task<IEnumerable<Tag>> FindBySubstring(string substring)
            => await FindByCondition(x => x.Name.Contains(substring), trackChanges: false)
                .ToArrayAsync();

        public async Task<IEnumerable<Tag>> CreateTags(IEnumerable<Tag> tags)
        {
           
        }*/
    }
}
