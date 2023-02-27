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
    
    
    public class ProjectController : ControllerBase
    {
        private readonly ACE42023Context db;    
    
        public ProjectController(ACE42023Context _db)
        {
            db = _db;           //dependicies injuction
        }

        [HttpGet]
        [Route("api/Employee" , Name = "ShowEmployee")]
        public async Task<ActionResult<IEnumerable<SuneetEmployee>>> ShowEmployee(){
            return Ok(await db.SuneetEmployees.ToListAsync());
        }

        [HttpGet]
        [Route("api/project/showuser/{id:int}", Name = "Getemp")]
        public async Task<ActionResult> GetUser(int? id){
             if(id == null) return BadRequest(new {Message = "Employee number is Required!"});
                SuneetEmployee b = await db.SuneetEmployees.FindAsync(id);
            if(b!=null){
                return Ok(b);
            }
            else{
                return NotFound();
            }
        }

        //ADMIN ONLY
        [HttpPost]
        [Route("api/project/admin/addemp", Name="AddEmployee")]
        
        public async Task<ActionResult> NewEmployee([FromBody] SuneetEmployee emp){
            if(emp == null){
                return BadRequest(new {Message = "Please enter all the required details"});
            }
            if(ModelState.IsValid){
                db.SuneetEmployees.Add(emp);
                await db.SaveChangesAsync();
                return Ok(emp);
            }
            else{
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/project/admin/edit/{id:int}", Name = "EditDetails")]
        public async Task<ActionResult> EditEmp(int? id , [FromBody] SuneetEmployee? emp){
           if(id == null) return BadRequest(new {Message = "Employee number is Required!"});
            SuneetEmployee e = await db.SuneetEmployees.FindAsync(id);
            if(emp == null){
                return BadRequest();
            }
                e.City = emp.City;
                e.DateOfBirth = emp.DateOfBirth;
                e.DeptId = emp.DeptId;
                e.Email = emp.Email;
                db.SuneetEmployees.Update(e);
                await db.SaveChangesAsync();
            return Ok(e); 
        }    

        [HttpDelete]
        [Route("api/project/admin/Delete/{id:int}", Name="DeleteEmp")]
        public async Task<ActionResult> DeleteEmployee(int? id){
            
            if(id == null) return BadRequest(new {Message = "Employee number is Required!"});
                SuneetEmployee b = await db.SuneetEmployees.FindAsync(id);
            if(b!=null){
                db.SuneetEmployees.Remove(b);
                await db.SaveChangesAsync();
            }
            else{
                return NotFound();
            }
            return NoContent();
        }



        // public IActionResult Create(){
        //     var dept = db.SuneetDepts.ToList();
        //     ViewBag.SuneetDepts = new SelectList(dept, "DeptId" , "Dname");
        //     var user_logged_in = HttpContext.Session.GetString("username");
        //     var logged_in_u = db.SuneetUsers.Where(x => x.Username == user_logged_in).SingleOrDefault();
        //     if(logged_in_u == null){
        //         return RedirectToAction("Login", "Login");
        //     }
        //     else if(logged_in_u.IsAdmin == true){
        //         return View();
        //     }
        //     else{
        //         return RedirectToAction("Login", "Login");
        //     }
        // }

        // [HttpPost]
        // public IActionResult Create(SuneetEmployee d)
        // {
        //     var user_logged_in = HttpContext.Session.GetString("username");
        //     var logged_in_u = db.SuneetUsers.Where(x => x.Username == user_logged_in).SingleOrDefault();
        //     if(logged_in_u == null){
        //         return RedirectToAction("Login", "Login");
        //     }
        //     else if(logged_in_u.IsAdmin == true){
        //         db.SuneetEmployees.Add(d);
        //         db.SaveChanges();
        //         return RedirectToAction("ShowEmployee");
        //     }
        //     else{
        //         return RedirectToAction("Login", "Login");
        //     }
        // }
        
        // public ActionResult SearchProducts(IFormCollection f){
        //     string keyword = f["keyword"].ToString();
        //     var result = db.SuneetEmployees.Where(x=>x.FirstName.Contains(keyword)).Select(x=>x).ToList();
        //     return View(result);
        // }

        // [Route("user/All_Details/{id}")]
        // public IActionResult Details(int id)
        // {
        //     SuneetEmployee d = db.SuneetEmployees.Find(id);
        //     return View(d);
        // }

        // [HttpGet]
        // public IActionResult Edit(int id)
        // {
        //     var dept = db.SuneetDepts.ToList();
        //     ViewBag.SuneetDepts = new SelectList(dept, "DeptId" , "Dname");
        //     var user_logged_in = HttpContext.Session.GetString("username");
        //     var logged_in_u = db.SuneetUsers.Where(x => x.Username == user_logged_in).SingleOrDefault();
        //     if(logged_in_u == null){
        //         return RedirectToAction("Login", "Login");
        //     }
        //     else if(logged_in_u.IsAdmin == true){
        //         SuneetEmployee b = db.SuneetEmployees.Where(x => x.EmployeeId == id).SingleOrDefault();
        //         return View(b);
        //     }
        //     else{
        //         return RedirectToAction("Login", "Login");
        //     }
        // }

        // [HttpPost]      //Data Annotations - compiler to know the following line is a get method
        // public ActionResult Edit(SuneetEmployee d)
        // {
        //     var dept = db.SuneetDepts.ToList();
        //     ViewBag.SuneetDepts = new SelectList(dept, "DeptId" , "Dname");
        //     var user_logged_in = HttpContext.Session.GetString("username");
        //     var logged_in_u = db.SuneetUsers.Where(x => x.Username == user_logged_in).SingleOrDefault();
        //         db.SuneetEmployees.Update(d);
        //         db.SaveChanges();
        //         return RedirectToAction("ShowEmployee");
        // }

        // public ActionResult Delete(int id)
        // {
        //     var dept = db.SuneetDepts.ToList();
        //     ViewBag.SuneetDepts = new SelectList(dept, "DeptId" , "Dname");
        //     var user_logged_in = HttpContext.Session.GetString("username");
        //     var logged_in_u = db.SuneetUsers.Where(x => x.Username == user_logged_in).SingleOrDefault();
        //     if(logged_in_u == null){
        //         return RedirectToAction("Login", "Login");
        //     }
        //     else if(logged_in_u.IsAdmin == true){
        //         SuneetEmployee d = db.SuneetEmployees.Find(id);
        //         return View(d);
        //     }
        //     else{
        //         return RedirectToAction("Login", "Login");
        //     }
        // }

        // [HttpPost]
        // [ActionName("Delete")] //mvc compiler
        // public ActionResult DeleteConfirmed(int id) //c# compiler alternative for method overloading
        // {       SuneetEmployee d = db.SuneetEmployees.Find(id);
        //         db.SuneetEmployees.Remove(d);
        //         db.SaveChanges();
        //         return RedirectToAction("ShowEmployee");       
        // }
    }
}
