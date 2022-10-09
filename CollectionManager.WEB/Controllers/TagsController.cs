using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    public class TagsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> FindTags(string substring)
        {
            var tags = await _unitOfWork.Tags.FindBySubstring(substring);

            return Json(tags.Select(x => x.Name));
        }
    }
}
