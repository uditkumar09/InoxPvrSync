using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PvrWebApp.Data;
using PvrWebApp.Model;

namespace PvrWebApp.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class PvrController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PvrController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        //[Route("[controller]")]
        [Route("api/[controller]")]
        [ActionName("PvrUsers")]
        //[Route("/")]
        public async Task<ActionResult<IEnumerable<PvrUser>>> PvrUsers()
        {
            var PvrUsers = _context.PvrUsers.ToListAsync();
            return await PvrUsers;
        }

        [HttpGet]
        //[Route("[controller]")]
        [Route("api/[controller]/pvruserid")]
        public IActionResult InoxUserbyId(int pvruserid)
        {
            var user = _context.PvrUsers.Find(pvruserid);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("[controller]")]
        public IActionResult AddUser(AddPvrUser adduser)
        {
            var user = new PvrUser()
            {
                Name = adduser.Name,
                Phone = adduser.Phone,
                Address = adduser.Address,
                JoinDate = adduser.JoinDate
            };
            _context.PvrUsers.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }


        [HttpPut]
        //[Route("[controller]")]
        [Route("api/[controller]")]
        public IActionResult UpdateUser(int pvruserid, UpdatePvrUser updatePvrUser)
        {
            var user = _context.PvrUsers.Find(pvruserid);
            if (user is null)
            {
                return NotFound();
            }

            user.Name = updatePvrUser.Name;
            user.Phone = updatePvrUser.Phone;
            user.Address = updatePvrUser.Address;
            user.JoinDate = updatePvrUser.JoinDate;
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpDelete]
        //[Route("[controller]")]
        [Route("api/[controller]/pvruserid")]
        public IActionResult DeleteUser(int pvruserid)
        {
            var user = _context.PvrUsers.Find(pvruserid);
            if (user is null)
            {
                return NotFound();
            }
            _context.PvrUsers.Remove(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpGet]
        [Route("~/PvrWebApp/InoxUsers")]
        public IActionResult SyncWithPvr()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7018/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("InoxUsers").Result;
                if (response.IsSuccessStatusCode)
                {
                    return Ok(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return BadRequest("error while seding data");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

    }
}
