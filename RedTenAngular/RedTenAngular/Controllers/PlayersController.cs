using AutoMapper;
using DAL;
using DAL.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedTenAngular.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public PlayersController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<PlayersController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Player> GetPlayers()
        {
            var players = this._unitOfWork.Players.GetAllPlayers(_unitOfWork.CurrentUserId);
            return players;
        }

        [HttpGet("{id}")]
        public Player GetPlayer(int id)
        {
            Player player = this._unitOfWork.Players.GetPlayer(id);
            return player;
        }
        
        [HttpPost]
        public IActionResult PostPlayer([Reqquired][FromBody]Player player)
        {
            int? groupid = this._unitOfWork.GroupUsers.GetGroupId(this._unitOfWork.CurrentUserId);
            if(groupid == null)
            {
                return BadRequest("Please create a group first");
            }
            this._unitOfWork.Players.AddPlayer(player);
            this._unitOfWork.SaveChanges();

            PlayerGroup playergroup = new PlayerGroup()
            {
                PlayerId = player.id,
                GroupId = groupid.Value
            };

            _unitOfWork.PlayerGroups.AddPlayerGroup(playergroup);
            _unitOfWork.SaveChanges();
            return Ok(player);
        }
    }
}
