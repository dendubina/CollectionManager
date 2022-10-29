using System.Threading.Tasks;
using CollectionManager.WEB.Models.Options;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CollectionManager.WEB.ViewComponents
{
    public class LargestCollectionsViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _collectionsToShowCount;

        public LargestCollectionsViewComponent(IUnitOfWork unitOfWork, IOptions<ViewOptions> options)
        {
            _collectionsToShowCount = options.Value.LargestCollectionsCount;
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
            => View("LargestCollectionsView", await _unitOfWork.Collections.GetMostLargeCollectionsAsync(_collectionsToShowCount));
    }
}
