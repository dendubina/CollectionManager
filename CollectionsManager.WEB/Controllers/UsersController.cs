﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CollectionManager.WEB.Extensions;
using CollectionManager.WEB.Models.Users;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            return View(_mapper.Map<IList<UserViewModel>>(users));
        }

        [HttpPost]
        public async Task<IActionResult> HandleChangingRoles(IList<UserViewModel> users, string submit)
        {
            var selectedUsersIds = users
                .Where(x => x.IsSelected)
                .Select(x => x.Id);

            switch (submit)
            {
                case "AddAdminRole":
                    await _unitOfWork.Users.AddAdminRoleAsync(selectedUsersIds, User.GetUserId());
                    break;
                case "RemoveAdminRole":
                    await _unitOfWork.Users.RemoveAdminRoleAsync(selectedUsersIds, User.GetUserId());
                    break;
            }

            return RedirectToAction("Index");
        }
    }
}
