using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkingGame.API.Models.DTOs;
using DrinkingGame.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrinkingGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            var gamesDto = new List<GameDto>();

            foreach (var game in games)
            {
                gamesDto.Add(new GameDto
                {
                    Id = game.Id,
                    GameName = game.GameName,
                    CreateDate = game.CreateDate,
                    CreatedBy = game.CreatedBy
                });                
            }

            return Ok(gamesDto);
        }
    }
}
