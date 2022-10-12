using System;

namespace CollectionsManager.DAL.Repositories.Interfaces
{
    public interface ICustomFieldValuesRepository
    {
        void DeleteCustomValuesForItem(Guid itemId);
    }
}
