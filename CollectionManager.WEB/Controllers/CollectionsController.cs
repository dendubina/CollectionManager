using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CollectionManager.WEB.Extensions;
using CollectionManager.WEB.Models.Collections;
using Contracts;
using Entities.DTO.Collections;
using Entities.EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.WEB.Controllers
{
    [Authorize]
    public class CollectionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CollectionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid userId)
        {
            var collections = await _unitOfWork.Collections.GetCollectionsByUser(userId);

            return View(_mapper.Map<IEnumerable<UsersCollectionToShow>>(collections));
        }

        [HttpGet]
        public async Task<IActionResult> CollectionDetails(Guid collectionId)
        {
            var collection = await _unitOfWork.Collections.GetCollectionDetails(collectionId);

            return View(_mapper.Map<CollectionDetailsToShow>(collection));
        }

        [HttpGet]
        public IActionResult CreateCollection()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCollection(CollectionToManipulateDto model)
        {
            var collection = _mapper.Map<Collection>(model,
                opt => opt.AfterMap((_, dest) => dest.OwnerId = User.GetUserId()));

            _unitOfWork.Collections.CreateCollection(collection);
            await _unitOfWork.SaveAsync();

            return RedirectToIndex();
        }

        [HttpGet]
        public async Task<IActionResult> EditCollection(Guid collectionId)
        {
            var collection = await _unitOfWork.Collections.GetCollectionDetails(collectionId);

            return View(_mapper.Map<CollectionToManipulateDto>(collection));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCollection(CollectionToManipulateDto model)
        {
            model.CustomFields = model.CustomFields.Where(x => x.ToRemove == false).ToList();

            var collection = await _unitOfWork.Collections
                .GetCollectionAsync(model.Id, trackChanges: true)
                .Include(x => x.CustomFields)
                .FirstOrDefaultAsync();

            _mapper.Map(model, collection);
            await _unitOfWork.SaveAsync();

            return RedirectToIndex();
        }

        public async Task<IActionResult> DeleteCollection(Guid collectionId)
        {
            _unitOfWork.Collections.DeleteCollection(new Collection{Id = collectionId});
            await _unitOfWork.SaveAsync();

            return RedirectToIndex();
        }

        private RedirectToActionResult RedirectToIndex()
            => RedirectToAction("Index", new { userId = User.GetUserId() });
    }
}
