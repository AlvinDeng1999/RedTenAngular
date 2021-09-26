using AutoMapper;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedTenAngular.Controllers
{
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
        public IEnumerable<Group> GetGroups()
        {
            var groups = _unitOfWork.Groups.GetAllGroups();
            return groups;
        }

        [HttpGet("{id}")]
        public Group GetGroup(int id)
        {
            Group group = this._unitOfWork.Groups.GetGroup(id);
            return group;
        }

        [HttpPost]
        public Group PostGroups(Group group)
        {
            this._unitOfWork.Groups.AddGroup(group);
            this._unitOfWork.SaveChanges();
            return group;
        }

        
    }
}
