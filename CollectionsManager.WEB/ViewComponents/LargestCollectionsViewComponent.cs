using System.Threading.Tasks;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.ViewComponents
{
    public class LargestCollectionsViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public LargestCollectionsViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
            => View("LargestCollectionsView", await _unitOfWork.Collections.GetMostLargeCollectionsAsync(5));
    }
}
