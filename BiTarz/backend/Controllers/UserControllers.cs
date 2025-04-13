using backend.Services;
using Microsoft.AspNetCore.Mvc;
using backend.Entities;

namespace backend.Controllers{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserControllers:ControllerBase
    {
        private readonly UserServices _userServices;
        public UserControllers(UserServices userServices){
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(){
            var users = await _userServices.GetUsersAsync();
            if(users == null){
                return NotFound("Kullanıcı Bulunamadı");
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetUserById(int id){
            var user = await _userServices.GetUserByIdAsync(id);
            if(user == null){
                return NotFound("Kullanıcı Bulunamadı");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User user){
            if(user == null){
                return BadRequest("Geçersiz kullanıcı verisi");
            }
            var createdUser = await _userServices.RegisterUserAsync(user, 2);
            return Ok(createdUser);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] User user){
            if(user == null){
                return BadRequest("Geçersiz kullanıcı verisi");
            }
            var loggedInUser = await _userServices.LoginUserAsync(user);
            if(loggedInUser == null){
                return NotFound("Kullanıcı Bulunamadı");
            }
            return Ok(loggedInUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id){
            var result = await _userServices.DeleteUserAsync(id);
            if(!result){
                return NotFound("Kullanıcı Bulunamadı");
            }
            return Ok("Kullanıcı silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user){
            if(user == null){
                return BadRequest("Geçersiz kullanıcı verisi");
            }
            var updatedUser = await _userServices.UpdateUserAsync(user);
            if(updatedUser == null){
                return NotFound("Kullanıcı Bulunamadı");
            }
            return Ok(updatedUser);
        }

        


    }

}