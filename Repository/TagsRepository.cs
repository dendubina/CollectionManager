using System.Collections.Generic;
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

        public async Task<IEnumerable<Tag>> GetAll()
            => await FindAll().ToArrayAsync();

        public async Task<IEnumerable<Tag>> FindBySubstring(string substring)
            => await FindByCondition(x => x.Name.Contains(substring), trackChanges: false)
                .ToArrayAsync();

        public Task CreateTags(IEnumerable<Tag> tags)
            => DbContext.Tags.AddRangeAsync(tags);
    }
}
