using System;
using System.Threading.Tasks;
using AutoMapper;
using CollectionManager.WEB.Extensions;
using Contracts;
using Entities.DTO.Collections;
using Entities.EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    [Authorize]
    public class UsersCollectionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersCollectionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(Guid userId)
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCollection()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCollection(CollectionToCreateDto model)
        {
            var userId = User.GetUserId(); 
            var entity = _mapper.Map<Collection>(model);
            entity.OwnerId = userId;

            _unitOfWork.Collections.CreateCollection(entity);
            await _unitOfWork.SaveAsync();
            
            return RedirectToAction("Index", new {userId = userId});
        } 
    }
}
