using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.Tags;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.BLL.Services
{
    public class TagsService : ITagsService
    {
        private readonly IRepositoryManager _unitOfWork;

        public TagsService(IRepositoryManager unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TagDto>> GetAllAsync()
            => await _unitOfWork.Tags
                .GetAll(trackChanges: false)
                .Select(tag => new TagDto { Name = tag.Name })
                .ToArrayAsync();

        public async Task<IEnumerable<TagDto>> FindBySubstringAsync(string substring)
            => await _unitOfWork.Tags
                .GetAll(trackChanges: false)
                .Where(x => x.Name.Contains(substring))
                .Select(tag => new TagDto { Name = tag.Name })
                .ToArrayAsync();
    }
}
