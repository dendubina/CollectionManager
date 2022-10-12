using System;
using System.Linq;
using CollectionsManager.DAL.EF;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;

namespace CollectionsManager.DAL.Repositories
{
    public class CustomFieldsValuesRepository : RepositoryBase<CustomFieldValue>, ICustomFieldValuesRepository
    {
        public CustomFieldsValuesRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void DeleteCustomValuesForItem(Guid itemId)
            => DbContext.CustomFieldValues.RemoveRange(DbContext.CustomFieldValues.Where(x => x.ItemId.Equals(itemId)));
    }
}
