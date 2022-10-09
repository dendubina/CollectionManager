using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.EF;
using Entities.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TagsRepository : RepositoryBase<Tag>, ITagsRepository
    {
        public TagsRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IQueryable<Tag> GetAll()
            => FindAll();

        public async Task<IEnumerable<Tag>> FindBySubstring(string substring)
            => await FindByCondition(x => x.Name.Contains(substring), trackChanges: false)
                .ToArrayAsync();

        public async Task<IEnumerable<Tag>> CreateTags(IEnumerable<Tag> tags)
        {
            var enumerable = tags as Tag[] ?? tags.ToArray();
            await DbContext.Tags.AddRangeAsync(enumerable.Except(await GetAll().ToArrayAsync()));
            await DbContext.SaveChangesAsync();

            return FindAll()
                .Where(x => enumerable.Select(n => n.Name).Any(f => f == x.Name))
                .ToList();
        }
        
    }
}
