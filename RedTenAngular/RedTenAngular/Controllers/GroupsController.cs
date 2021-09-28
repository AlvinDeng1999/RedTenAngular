using AutoMapper;
using DAL;
using DAL.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedTenAngular.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedTenAngular.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public GroupsController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GroupsController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<GroupViewModel> GetGroups()
        {
            var groups = _unitOfWork.Groups.GetAllGroups(_unitOfWork.CurrentUserId).ToList();
            var players = _unitOfWork.Players.GetAllPlayers(_unitOfWork.CurrentUserId).ToList();
            var retModel = groups.Select(g => new GroupViewModel(g) { Players = players }).ToList();
            return retModel;
        }

        [HttpGet("{id}")]
        public Group GetGroup(int id)
        {
            Group group = this._unitOfWork.Groups.GetGroup(id);
            return group;
        }

        [HttpPost]
        public IActionResult PostGroups(Group group)
        {
            if(this._unitOfWork.GroupUsers.UserHasGroup(this._unitOfWork.CurrentUserId))
            {
                return BadRequest("User already has a group");
            }
            this._unitOfWork.Groups.AddGroup(group);
            this._unitOfWork.SaveChanges();
            GroupUser groupuser = new GroupUser()
            {
                GroupId = group.id,
                userId = this._unitOfWork.CurrentUserId
            };
            
            this._unitOfWork.GroupUsers.AddGroupUser(groupuser);
            this._unitOfWork.SaveChanges();
            return Ok(group);
        }

        
    }
}
