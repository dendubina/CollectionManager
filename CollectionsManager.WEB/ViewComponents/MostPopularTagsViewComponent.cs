using System.Threading.Tasks;
using CollectionManager.WEB.Models.Options;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CollectionManager.WEB.ViewComponents
{
    public class MostPopularTagsViewComponent : ViewComponent
    {
        private readonly int _tagsCountToShow;
        private readonly IUnitOfWork _unitOfWork;

        public MostPopularTagsViewComponent(IUnitOfWork unitOfWork, IOptions<ViewOptions> options)
        {
            _unitOfWork = unitOfWork;
            _tagsCountToShow = options.Value.PopularTagsToShowCount;
        }

        public async Task<IViewComponentResult> InvokeAsync()
            => View("MostPopularTagsView", await _unitOfWork.Tags.GetMostPopularTagsAsync(_tagsCountToShow));
    }
}
