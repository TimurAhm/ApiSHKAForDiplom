using ApiSHKAForDiplom.Database;
using ApiSHKAForDiplom.Database.Entity;
using ApiSHKAForDiplom.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiSHKAForDiplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        public EfModel _efModel;

        public UsersController(EfModel model)
        {
            _efModel = model;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _efModel.UserR.ToListAsync();
        }

        //[HttpGet("Users")]
        //public async Task<ActionResult<List<User>>> GetUsersAsync()
        //{

        //    // return await _efModel.UserR.Include(i => i.UsersBankNavigation).ToListAsync();
        //    return null;
        //}

        [HttpGet("id")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (User == null)
                return NotFound();
            return await _efModel.UserR.FindAsync(id);
        }

        [HttpPost("PostUser")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _efModel.UserR.Add(user);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostUser), new { id = user.UserId }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _efModel.UserR.FindAsync(id);
            if (user == null)
                return NotFound();
            _efModel.UserR.Remove(user);
            await _efModel.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("PutUser")]
        public async Task<IActionResult> PutUser(long IdUUSSEERR, User user)
        {
            if (IdUUSSEERR != user.UserId)
            {
                return BadRequest();
            }
            _efModel.Entry(user).State = EntityState.Modified;
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

        //[HttpPost("/token")]
        //public ActionResult<object> Token(string username, string password)
        //{
        //    //Get identity
        //    var identity = GetIdentity(username, password);

        //    if (identity == null)
        //    { 
        //        return BadRequest();
        //    }

        //    //If identity not null
        //    var now = DateTime.UtcNow;
        //    //Compile jwt token
        //    var jwt = new JwtSecurityToken
        //        (
        //        audience: TokenOptionsClass.AUDIENCE,
        //        issuer: TokenOptionsClass.ISSUER,
        //        notBefore: now,
        //        claims: identity.Claims,
        //        expires: now.Add(TimeSpan.FromMinutes(TokenOptionsClass.LIFETIME)),
        //        signingCredentials: new SigningCredentials(TokenOptionsClass.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        //    //return token and username 
        //    var response = new
        //    {
        //        access_token = encodedJwt,
        //        username = identity.Name
        //    };
        //    return response;
        //}

        //[NonAction]
        //private ClaimsIdentity GetIdentity(string login, string password)
        //{
        //    //Get user from database
        //    User user = _efModel.UserR.FirstOrDefault(x => x.UserLogin == login && x.UserPassword == password);
        //    //If user is not empty
        //    if (user != null)
        //    {
        //        //Set claims
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserFam + " " + user.UserName + " " + user.UserOtch),
        //            new Claim("UserId", user.UserId.ToString())
        //        };
        //        //Compile ClaimIdentity
        //        ClaimsIdentity claimsIdentity =
        //            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        //        return claimsIdentity;
        //    }
        //    return null;
        //}
        //[Authorize]
        //[HttpGet("/getuser")]
        //public async Task<ActionResult<User>> getUserAsync()
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;
        //    if (identity == null)
        //        throw new Exception("Identity пустой");
        //    return await _efModel.UserR.FindAsync(Convert.ToInt32(identity.FindFirst("UserId").Value));
        //}
    }
}
