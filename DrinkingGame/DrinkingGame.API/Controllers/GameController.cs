using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DrinkingGame.API.Models.Domain;
using DrinkingGame.API.Models.DTOs;
using DrinkingGame.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var gameDto = await _gameRepository.GetById(id);
            if (gameDto.GameName.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(gameDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateGameRequestDto requestDto)
        {
            var updatedGame = await _gameRepository.UpdateAsync(id, requestDto);
            return updatedGame.GameName.IsNullOrEmpty() ? NotFound() : Ok(updatedGame);
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
