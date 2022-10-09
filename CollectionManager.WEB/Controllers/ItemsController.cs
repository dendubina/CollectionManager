using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTO.Items;
using Entities.EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        {
            var item = await _unitOfWork.Items.GetItemDetailsAsync(itemId);

            return View(_mapper.Map<ItemDetailsToReturnDto>(item));
        }

        [HttpGet]
        public async Task<IActionResult> AddItemToCollection(Guid collectionId)
        {
            var collectionDetails = await _unitOfWork.Collections.GetCollectionDetailsAsync(collectionId);

            return View(_mapper.Map<ItemToCreate>(collectionDetails));
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCollection(ItemToCreate item)
        {
            var entity = _mapper.Map<Item>(item);

            _unitOfWork.Items.CreateItem(entity);

            if (item.Tags is not null && item.Tags.Any())
            {
                var tags = await _unitOfWork.Tags.CreateTags(item.Tags);
                entity.Tags = tags.ToList();
            }

            await _unitOfWork.SaveAsync();

            return RedirectToCollectionDetails(item.CollectionId);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid itemId)
        {
            var item = await _unitOfWork.Items.GetItem(itemId, trackChanges: false)
                .Include(x => x.CustomValues)
                .ThenInclude(x => x.Field)
                .FirstOrDefaultAsync();

            return View(_mapper.Map<ItemToEditDto>(item));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemToEditDto model)
        {
            var item = await _unitOfWork.Items.GetItem(model.Id, trackChanges: true).FirstOrDefaultAsync();

            _mapper.Map(model, item);
            await _unitOfWork.SaveAsync();

            return RedirectToCollectionDetails(item.CollectionId);
        }

        public async Task<IActionResult> Delete(Guid itemId, Guid collectionId)
        {
            _unitOfWork.Items.DeleteItem(new Item{Id = itemId});
            await _unitOfWork.SaveAsync();

            return RedirectToCollectionDetails(collectionId);
        }

        private RedirectToActionResult RedirectToCollectionDetails(Guid collectionId)
            => RedirectToAction("CollectionDetails", "Collections", new {collectionId});
    }
}
