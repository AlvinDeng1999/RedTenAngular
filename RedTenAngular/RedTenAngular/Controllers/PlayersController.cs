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
            var players = this._unitOfWork.Players.GetAllPlayers();
            return players;
        }

        [HttpGet("{id}")]
        public Player GetPlayer(int id)
        {
            Player player = this._unitOfWork.Players.GetPlayer(id);
            return player;
        }
        
        [HttpPost]
        public Player PostPlayer(Player player)
        {
            this._unitOfWork.Players.AddPlayer(player);
            this._unitOfWork.SaveChanges();
            return player;
        }
    }
}
