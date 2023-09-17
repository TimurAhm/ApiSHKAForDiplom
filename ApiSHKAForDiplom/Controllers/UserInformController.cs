using ApiSHKAForDiplom.Database.Entity;
using ApiSHKAForDiplom.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ApiSHKAForDiplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInformController : Controller
    {
        public EfModel _efModel;

        public UserInformController(EfModel model)
        {
            _efModel = model;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserInform>>> GetUserInform()
        {
            return await _efModel.UserInformM.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<UserInform>> PostUserInform(UserInform userInform)
        {
            _efModel.UserInformM.Add(userInform);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostUserInform), userInform); // если будет баг с добавлением, тот он тут (в этой строчке)
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserInform(int UserInformUserId)
        {
            var userInform = await _efModel.UserInformM.FindAsync(UserInformUserId); // тут тоже прикол, тут было удаление по id, а у меня в табл балансов 
            if (userInform == null)                                            // как первичный ключ строка с макс длинной 10, если бал, тот тут
                return NotFound();
            _efModel.UserInformM.Remove(userInform);
            await _efModel.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutUserInformBank(int UserInformUserId, UserInform userInform)
        {
            if (UserInformUserId != userInform.UserInformUserId)
            {
                return BadRequest();
            }
            _efModel.Entry(userInform).State = EntityState.Modified;
            try
            {
                await _efModel.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
