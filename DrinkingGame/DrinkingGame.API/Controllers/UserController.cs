using AutoMapper;
using DrinkingGame.API.Models.DTOs;
using DrinkingGame.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DrinkingGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper; 

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return Ok(_mapper.Map<List<UserDto>>(users));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user.UserName.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddUserRequestDto requestDto)
        {
            var createdUser = await _userRepository.CreateAsync(requestDto);

            //return Ok(createdUser);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateUserRequestDto requestDto)
        {
            var updatedUser = await _userRepository.UpdateAsync(id, requestDto);
            if (updatedUser.UserName.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAysnc([FromRoute] Guid id)
        {
            var userWasDeleted = await _userRepository.DeleteAsync(id);

            if (!userWasDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
