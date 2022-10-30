using System.Threading.Tasks;
using CollectionManager.WEB.Models.Options;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CollectionManager.WEB.ViewComponents
{
    public class LastAddedItemsViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _itemsToShowCount;

        public LastAddedItemsViewComponent(IUnitOfWork unitOfWork, IOptions<ViewOptions> options)
        {
            _itemsToShowCount = options.Value.LastAddedItemsCount;
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
            => View("LastAddedItemsView", await _unitOfWork.Items.GetLastAddedItemsAsync(_itemsToShowCount));
    }
}
