using System;

namespace Contracts
{
    public interface ICustomFieldValuesRepository
    {
        void DeleteCustomFieldForItem(Guid itemId);
    }
}
