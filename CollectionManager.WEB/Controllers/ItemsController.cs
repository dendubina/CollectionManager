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
        public async Task<IActionResult> AddItemToCollection(Guid collectionId)
        {
            var entity = await _unitOfWork.Collections.GetCollectionAsync(collectionId, trackChanges: false);

            var model = _mapper.Map<ItemToCreate>(entity);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItemToCollection(ItemToCreate item)
        {
            var entity = _mapper.Map<Item>(item);

            _unitOfWork.Items.CreateItem(entity);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
