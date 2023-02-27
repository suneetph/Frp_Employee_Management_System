using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstapi.Models;

namespace firstapi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ACE42023Context db;
        public UserController( ACE42023Context _db)
        {
            db = _db;
        }
        

        [HttpPost]
        [Route("project/newuser", Name = "NewUser")]
        public async Task<ActionResult> Create(SuneetUser? u){
            //Check VAlidation errors
            if(u == null){
                return BadRequest(new {Message = "Please Provide all the required details!"});
            }

            var rightuser = db.SuneetUsers.Where(x => x.Username == u.Username).SingleOrDefault();
            if(rightuser != null){
                return BadRequest(new {Message = "User Exists!"});
            }
            if(ModelState.IsValid){
                db.SuneetUsers.Add(u);
                await db.SaveChangesAsync();
                return Ok(u);
            }else{
                return BadRequest("Please Provide details correctly. ");
            }
        }
        
        [HttpPost]
        [Route("project/user", Name = "GetUser")]
        public ActionResult GetUser([FromBody] string? uname){
            if(uname == null){
                return BadRequest(new {Message = "Please provide username!"});
            }
            var check_u = db.SuneetUsers.Where(x => x.Username == uname).SingleOrDefault();
            if(check_u != null){
                return Ok(check_u.UserId);
            }
            else{
                return BadRequest("Please provide valid username!");
            }
        }
        
    }
}
