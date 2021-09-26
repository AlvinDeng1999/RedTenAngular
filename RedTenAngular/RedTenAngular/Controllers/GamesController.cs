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
    public class GamesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public GamesController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GamesController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            var games = _unitOfWork.Games.GetAllGames();
            return games;
        }

        [HttpGet("{id}")]
        public Game GetGame(int id)
        {
            Game game = this._unitOfWork.Games.GetGame(id);
            return game;
        }

        [HttpPost]
        public Game PostGames(Game game)
        {
            this._unitOfWork.Games.AddGame(game);
            this._unitOfWork.SaveChanges();
            return game;
        }
    }
}
