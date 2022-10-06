using System;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTO.Items;
using Entities.EF.Models;
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
        public async Task<IActionResult> Details(Guid itemId)
        {
            var item = await _unitOfWork.Items.GetItemDetailsAsync(itemId);

            var model = _mapper.Map<ItemDetailsToReturnDto>(item);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddItemToCollection(Guid collectionId)
        {
            var collectionDetails = await _unitOfWork.Collections.GetCollectionDetails(collectionId);

            var model = _mapper.Map<ItemToCreate>(collectionDetails);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
