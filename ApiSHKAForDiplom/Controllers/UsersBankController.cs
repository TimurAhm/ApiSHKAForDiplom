using ApiSHKAForDiplom.Database.Entity;
using ApiSHKAForDiplom.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ApiSHKAForDiplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersBankController : Controller
    {
        public EfModel _efModel;

        public UsersBankController(EfModel model)
        {
            _efModel = model;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UsersBank>>> GetUsersBank()
        {
            return await _efModel.UsersBankK.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<UsersBank>> PostUsersBank(UsersBank usersBank)
        {
            _efModel.UsersBankK.Add(usersBank);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostUsersBank), usersBank); // если будет баг с добавлением, тот он тут (в этой строчке)
        }

        [HttpDelete("{UsersBankRef}")]
        public async Task<IActionResult> DeleteUsersBank(string UsersBankRef)
        {
            var usersBank = await _efModel.UsersBankK.FindAsync(UsersBankRef); // тут тоже прикол, тут было удаление по id, а у меня в табл балансов 
            if (usersBank == null)                                            // как первичный ключ строка с макс длинной 10, если бал, тот тут
                return NotFound();
            _efModel.UsersBankK.Remove(usersBank);
            await _efModel.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("UsersBankRef")]
        public async Task<IActionResult> PutUsersBank(string UsersBankRef, UsersBank userBank)
        {
            if (UsersBankRef != userBank.UsersBankRef)
            {
                return BadRequest();
            }
            _efModel.Entry(userBank).State = EntityState.Modified;
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
