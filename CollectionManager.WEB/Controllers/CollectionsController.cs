using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CollectionManager.WEB.Extensions;
using CollectionManager.WEB.Models.Collections;
using Contracts;
using Entities.DTO.Collections;
using Entities.EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var collection = await _unitOfWork.Collections.GetCollectionAsync(collectionId, trackChanges: false);

            return View(_mapper.Map<CollectionDetailsToShow>(collection));
        }

        [HttpGet]
        public IActionResult CreateCollection()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCollection(CollectionToCreateDto model)
        {
            var entity = _mapper.Map<Collection>(model,
                opt => opt.AfterMap((_, dest) => dest.OwnerId = User.GetUserId()));

            _unitOfWork.Collections.CreateCollection(entity);
            await _unitOfWork.SaveAsync();
            
            return RedirectToAction("Index", new {userId = User.GetUserId()});
        }

        public async Task<IActionResult> DeleteCollection(Guid collectionId)
        {
            var entity = await _unitOfWork.Collections.GetCollectionAsync(collectionId, trackChanges: false);

            _unitOfWork.Collections.DeleteCollection(entity);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index", new { userId = User.GetUserId() });
        }
    }
}
