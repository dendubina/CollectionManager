using System;
using System.Threading.Tasks;
using AutoMapper;
using CollectionManager.WEB.Extensions;
using CollectionsManager.BLL.DTO.Items;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

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
        {
            var collectionDetails = await _unitOfWork.Collections.GetCollectionDetailsAsync(collectionId);

            return View(_mapper.Map<ItemToCreate>(collectionDetails));
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCollection(ItemToCreate item)
        {
            await _unitOfWork.Items.CreateItemAsync(item);

            return RedirectToCollectionDetails(item.CollectionId);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid itemId)
            => View(await _unitOfWork.Items.GetItemToEditAsync(itemId));

        [HttpPost]
        public async Task<IActionResult> Edit(ItemToEditDto model)
        {
            model.CurrentUserId = User.GetUserId();
            await _unitOfWork.Items.UpdateItemAsync(model);

            return RedirectToAction("Index", "Home");
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
