using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CollectionManager.WEB.Extensions;
using CollectionManager.WEB.Models.Items;
using CollectionsManager.BLL.DTO.Items;
using CollectionsManager.BLL.DTO.Tags;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ItemsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid itemId)
            => View(await _unitOfWork.Items.GetItemDetailsAsync(itemId));

        [HttpGet]
        public async Task<IActionResult> AddItemToCollection(Guid collectionId)
            => View(await _unitOfWork.Items.GetItemToAddAsync(collectionId));

        [HttpPost]
        public async Task<IActionResult> AddItemToCollection(ItemToCreate item)
        {
            if (!ModelState.IsValid)
                return View(item);

            await _unitOfWork.Items.CreateItemAsync(item);

            return RedirectToCollectionDetails(item.CollectionId);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid itemId)
        {
            var item = await _unitOfWork.Items.GetItemToEditAsync(itemId);

            return View(_mapper.Map<ItemToEditViewModel>(item));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemToEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = _mapper.Map<ItemToEditDto>(model);
            dto.CurrentUserId = User.GetUserId();

            var tags = model.ExistedTags
                .Where(x => x.ToRemove == false)
                .Select(x => x.Name);

            if (model.Tags is not null)
            {
                tags = tags.Union(model.Tags.Select(x => x.Name));
            }
               
            dto.Tags = tags.Select(tagName => new TagDto { Name = tagName });

            await _unitOfWork.Items.UpdateItemAsync(dto);

            return RedirectToAction("Details", new { itemId = model.Id });
        }

        public async Task<IActionResult> Delete(Guid itemId, Guid collectionId)
        {
            await _unitOfWork.Items.DeleteItemAsync(itemId, User.GetUserId());

            return RedirectToCollectionDetails(collectionId);
        }

        private RedirectToActionResult RedirectToCollectionDetails(Guid collectionId)
            => RedirectToAction("CollectionDetails", "Collections", new { collectionId });
    }
}
