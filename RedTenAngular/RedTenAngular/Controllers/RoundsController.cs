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
    public class RoundsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public RoundsController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<RoundsController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Round> GetRounds()
        {
            var rounds = this._unitOfWork.Rounds.GetAllRounds();
            return rounds;
        }

        [HttpGet("{id}")]
        public Round GetRound(int id)
        {
            Round round = this._unitOfWork.Rounds.GetRound(id);
            return round;
        }

        [HttpPost]
        public IActionResult PostRound(Round round)
        {
            int? recentGameId = this._unitOfWork.Games.GetGameId(this._unitOfWork.CurrentUserId);
            if (recentGameId == null) return BadRequest("Please create a game first");
            round.GameId = recentGameId.Value;
            this._unitOfWork.Rounds.AddRound(round);
            this._unitOfWork.SaveChanges();
            return Ok(round);
        }
    }
}
