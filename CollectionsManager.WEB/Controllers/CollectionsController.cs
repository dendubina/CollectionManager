using System;
using System.Linq;
using System.Threading.Tasks;
using CollectionManager.WEB.Extensions;
using CollectionManager.WEB.Models;
using CollectionsManager.BLL.DTO.Collections;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    [Authorize]
    public class CollectionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CollectionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> UsersCollections(Guid userId)
            => View(await _unitOfWork.Collections.GetCollectionsByUserAsync(userId));

        [HttpPost]
        public IActionResult GetMarkDownPreview([FromBody] MarkDownViewModel model)
            => PartialView(model);

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> CollectionDetails(Guid collectionId)
            => View(await _unitOfWork.Collections.GetCollectionDetailsAsync(collectionId));

        [HttpGet]
        public IActionResult CreateCollection()
            => View();

        [HttpPost]
        public async Task<IActionResult> CreateCollection(CollectionToManipulateDto model)
        {
            model.OwnerId = User.GetUserId();

            await _unitOfWork.Collections.CreateCollectionAsync(model);

            return RedirectToIndex();
        }

        [HttpGet]
        public async Task<IActionResult> EditCollection(Guid collectionId)
            => View(await _unitOfWork.Collections.GetCollectionToEditAsync(collectionId));

        [HttpPost]
        public async Task<IActionResult> EditCollection(CollectionToManipulateDto model)
        {
            model.CustomFields = model.CustomFields?.Where(x => x.ToRemove == false).ToList();
            model.OwnerId = User.GetUserId();

            await _unitOfWork.Collections.UpdateCollectionAsync(model);

            return RedirectToIndex();
        }

        public async Task<IActionResult> DeleteCollection(Guid collectionId)
        {
            await _unitOfWork.Collections.DeleteCollectionAsync(collectionId, User.GetUserId());

            return RedirectToIndex();
        }

        private RedirectToActionResult RedirectToIndex()
            => RedirectToAction("UsersCollections", new { userId = User.GetUserId() });
    }
}
