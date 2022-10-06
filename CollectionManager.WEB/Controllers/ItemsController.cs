using System;
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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid itemId)
        {
            var item = await _unitOfWork.Items.GetItem(itemId, trackChanges: false)
                .Include(x => x.CustomValues)
                .ThenInclude(x => x.Field)
                .FirstOrDefaultAsync();

            var model = _mapper.Map<ItemToEditDto>(item);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemToEditDto model)
        {
            var item = await _unitOfWork.Items.GetItem(model.Id, trackChanges: true)
                .Include(x => x.CustomValues)
                //.ThenInclude(x => x.Field)
                .FirstOrDefaultAsync();

            _mapper.Map(model, item);
            await _unitOfWork.SaveAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCollection(ItemToCreate item)
        {
            var entity = _mapper.Map<Item>(item);

           /* foreach (var value in entity.CustomValues)
            {
                value.Item = entity;
            }*/

            _unitOfWork.Items.CreateItem(entity);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("CollectionDetails", "Collections", new {collectionId = item.CollectionId});
        }

        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            _unitOfWork.Items.DeleteItem(new Item{Id = itemId});
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
