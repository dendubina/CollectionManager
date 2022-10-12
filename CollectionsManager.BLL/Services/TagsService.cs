using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;

namespace CollectionsManager.BLL.Services
{
    public class TagsService : ITagsService
    {
        private readonly IRepositoryManager _unitOfWork;

        public TagsService(IRepositoryManager unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Tag>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        
        public Task<IEnumerable<Tag>> FindBySubstringAsync(string substring)
        {
            throw new NotImplementedException();
        }
    }
}
