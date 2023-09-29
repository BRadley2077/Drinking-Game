using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DrinkingGame.API.Models.Domain;
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
        private readonly IMapper _mapper;

        public GameController(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();

            return Ok(_mapper.Map<List<GameDto>>(games));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddGameRequestDto requestDto)
        {
            var newGame = _mapper.Map<Game>(requestDto);

            await _gameRepository.CreateAsync(newGame);
            
            return Ok(_mapper.Map<GameDto>(newGame));
        }
    }
}
