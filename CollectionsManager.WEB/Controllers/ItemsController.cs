using System;
using System.Collections.Generic;
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
        private readonly ISearchService _searchService;

        public ItemsController(IUnitOfWork unitOfWork, IMapper mapper, ISearchService searchService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _searchService = searchService;
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
            var tags = new List<string>();

            if (model.ExistedTags is not null)
            {
                tags = model.ExistedTags
                    .Where(x => x.ToRemove == false)
                    .Select(x => x.Name)
                    .ToList();
            }

            if (model.Tags is not null)
            {
                tags = tags.Union(model.Tags.Select(x => x.Name)).ToList();
            }
               
            dto.Tags = tags.Select(tagName => new TagDto { Name = tagName });

            await _unitOfWork.Items.UpdateItemAsync(dto);

            return RedirectToAction("Details", new { itemId = model.Id });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string substring)
        {
            var items = await _searchService.SearchBySubstringAsync(substring);

            return View("SearchResult", _mapper.Map<IEnumerable<FoundItemToReturnDto>>(items));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SearchByTag(string tag)
            => View("SearchResult", await _unitOfWork.Items.GetByTagAsync(tag));

        public async Task<IActionResult> Delete(Guid itemId, Guid collectionId)
        {
            await _unitOfWork.Items.DeleteItemAsync(itemId, User.GetUserId());

            return RedirectToCollectionDetails(collectionId);
        }

        private RedirectToActionResult RedirectToCollectionDetails(Guid collectionId)
            => RedirectToAction("CollectionDetails", "Collections", new { collectionId });
    }
}
