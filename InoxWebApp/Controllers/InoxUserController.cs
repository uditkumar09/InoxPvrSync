using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InoxWebApp.Data;
using InoxWebApp.Model;

namespace InoxWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InoxUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InoxUserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/InoxUsers
        [HttpGet]
        //[Route("[controller]")]
        //[Route("[InoxUsers]")]
        //[ActionName("InoxUsers")]
        //[Route("/")]
        public async Task<ActionResult<IEnumerable<InoxUser>>> InoxUsers()
        {
          if (_context.InoxUsers == null)
          {
              return NotFound();
          }
            var InoxUsers = _context.InoxUsers.ToListAsync();
            return await InoxUsers;
        }

        // GET: api/InoxUsers/5
        [HttpGet, Route("InoxUser")]
        //[Route("[controller]")]
        //[Route("[controller]/inoxuserid")]
        public  IActionResult InoxUser(int inoxuserid)
        {
            if (_context.InoxUsers == null)
            {
                return NotFound();
            }
            var inoxUser =  _context.InoxUsers.Find(inoxuserid);

            if (inoxUser is null)
            {
                return NotFound();
            }

            return Ok(inoxUser);
        }

        // PUT: api/InoxUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Route("[controller]")]
        [HttpPut, Route("PutInoxUser")]
        //[Route("[controller]")]
        public async Task<IActionResult> PutInoxUser(int id, UpdateInoxUser inoxUser)
        {
            var user = _context.InoxUsers.Find(id);
            if (user is null)
            { 
                return NotFound(id);
            }

            try
            {
                user.Name = inoxUser.Name;
                user.Phone = inoxUser.Phone;
                user.Address = inoxUser.Address;
                user.JoinDate = inoxUser.JoinDate;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InoxUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(user);
        }

        // POST: api/InoxUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Route("PostInoxUser")]
        //[Route("[controller]")]
        public async Task<ActionResult<InoxUser>> PostInoxUser(AddInoxUser inoxUser)
        {
            if (_context.InoxUsers == null)
            {
                return Problem("Entity set 'AppDbContext.InoxUsers'  is null.");
            }

            var user = new InoxUser()
            {
                Name = inoxUser.Name,
                Phone = inoxUser.Phone,
                Address = inoxUser.Address,
                JoinDate = inoxUser.JoinDate
            };
            _context.InoxUsers.Add(user);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetInoxUser", new { id = inoxUser.InoxUserId }, inoxUser);
            return Ok(user);
        }

        // DELETE: api/InoxUsers/5
        [HttpDelete, Route("DeleteInoxUser")]
        //[Route("[controller]")]
        //[Route("[controller]/pvruserid")]
        public async Task<IActionResult> DeleteInoxUser(int id)
        {
            if (_context.InoxUsers == null)
            {
                return NotFound();
            }
            var inoxUser = await _context.InoxUsers.FindAsync(id);
            if (inoxUser == null)
            {
                return NotFound();
            }

            _context.InoxUsers.Remove(inoxUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet, Route("~/InoxWebApp/PvrUsers")]
        //[Route("~/InoxWebApp/PvrUsers")]
        public IActionResult SyncWithInox()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7127/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("PvrUsers").Result;
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

        private bool InoxUserExists(int id)
        {
            return (_context.InoxUsers?.Any(e => e.InoxUserId == id)).GetValueOrDefault();
        }
    }
}
