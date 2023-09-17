using ApiSHKAForDiplom.Database.Entity;
using ApiSHKAForDiplom.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSHKAForDiplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredController : Controller
    {
        public EfModel _efModel;

        public CredController(EfModel model)
        {
            _efModel = model;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Credit>>> GetCredits()
        {
            return await _efModel.CreditT.ToListAsync();
        }

        //[HttpPost]
        //public async Task<ActionResult<Credit>> PostCredit(Credit credit)
        //{
        //    _efModel.CreditT.Add(credit);
        //    await _efModel.SaveChangesAsync();

        //    return CreatedAtAction(nameof(PostCredit), credit); // если будет баг с добавлением, тот он тут (в этой строчке)
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteCredit(int CreditUserId)
        //{
        //    var credit = await _efModel.CreditT.FindAsync(CreditUserId); // тут тоже прикол, тут было удаление по id, а у меня в табл балансов 
        //    if (credit == null)                                            // как первичный ключ строка с макс длинной 10, если бал, тот тут
        //        return NotFound();
        //    _efModel.CreditT.Remove(credit);
        //    await _efModel.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpPut]
        //public async Task<IActionResult> PutCredit(int CreditUserId, Credit credit)
        //{
        //    if (CreditUserId != credit.CreditUserId)
        //    {
        //        return BadRequest();
        //    }
        //    _efModel.Entry(credit).State = EntityState.Modified;
        //    try
        //    {
        //        await _efModel.SaveChangesAsync();
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //    return NoContent();
        //}
    }
}
