using System.Threading.Tasks;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.ViewComponents
{
    public class LastAddedItemsViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public LastAddedItemsViewComponent(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<IViewComponentResult> InvokeAsync()
            => View("LastAddedItemsView", await _unitOfWork.Items.GetLastAddedItems(5));
    }
}
