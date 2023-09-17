using ApiSHKAForDiplom.Database.Entity;
using ApiSHKAForDiplom.Database;
using ApiSHKAForDiplom.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace ApiSHKAForDiplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : Controller
    {
        public EfModel _efModel;

        public WorkerController(EfModel model)
        {
            _efModel = model;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Worker>>> GetWorkers()
        {
            return await _efModel.WorkerR.ToListAsync();
        }

        [HttpPost("PostWorker")]
        public async Task<ActionResult<Worker>> PostWorker(Worker worker)
        {
            _efModel.WorkerR.Add(worker);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostWorker), new { id = worker.WorkerId }, worker);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var worker = await _efModel.WorkerR.FindAsync(id);
            if (worker == null)
                return NotFound();
            _efModel.WorkerR.Remove(worker);
            await _efModel.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("PutWorker")]
        public async Task<IActionResult> PutWorker(long IdUUSSEERR, Worker worker)
        {
            if (IdUUSSEERR != worker.WorkerId)
            {
                return BadRequest();
            }
            _efModel.Entry(worker).State = EntityState.Modified;
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

        [HttpPost("/token")]
        public ActionResult<object> Token(string workername, string password)
        {
            //Get identity
            var identity = GetIdentity(workername, password);
            string userrole = String.Empty;
            if (identity == null)
            {
                return BadRequest();
            }

            //If identity not null
            var now = DateTime.UtcNow;
            //Compile jwt token
            var jwt = new JwtSecurityToken
                (
                audience: TokenOptionsClass.AUDIENCE,
                issuer: TokenOptionsClass.ISSUER,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(TokenOptionsClass.LIFETIME)),
                signingCredentials: new SigningCredentials(TokenOptionsClass.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            //return token and username 
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return response;
        }

        [NonAction]
        private ClaimsIdentity GetIdentity(string login, string password)
        {
            //Get user from database
            Worker worker = _efModel.WorkerR.FirstOrDefault(x => x.WorkerLogin == login && x.WorkerPassword == password);
            //If user is not empty
            if (worker != null)
            {
                //Set claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, worker.WorkerRole),
                    new Claim("WorkerId", worker.WorkerId.ToString())
                };
                //Compile ClaimIdentity
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
        [Authorize]
        [HttpGet("/getworker")]
        public async Task<ActionResult<Worker>> getWorkerAsync()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                throw new Exception("Identity пустой");
            return await _efModel.WorkerR.FindAsync(Convert.ToInt32(identity.FindFirst("WorkerId").Value));
        }
    }
}
