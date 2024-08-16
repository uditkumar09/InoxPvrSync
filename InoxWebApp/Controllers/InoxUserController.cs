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
    //[Route("[controller]")]
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
        [Route("api/[controller]")]
        [ActionName("InoxUsers")]
        //[Route("/")]
        public async Task<ActionResult<IEnumerable<InoxUser>>> InoxUsers()
        {
          if (_context.InoxUsers == null)
          {
              return NotFound();
          }
            return await _context.InoxUsers.ToListAsync();
        }

        // GET: api/InoxUsers/5
        [HttpGet]
        //[Route("[controller]")]
        [Route("api/[controller]/inoxuserid")]
        public async Task<ActionResult<InoxUser>> InoxUser(int inoxuserid)
        {
          if (_context.InoxUsers == null)
          {
              return NotFound();
          }
            var inoxUser = await _context.InoxUsers.FindAsync(inoxuserid);

            if (inoxUser == null)
            {
                return NotFound();
            }

            return inoxUser;
        }

        // PUT: api/InoxUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Route("[controller]")]
        [Route("api/[controller]")]
        public async Task<IActionResult> PutInoxUser(int id, InoxUser inoxUser)
        {
            if (id != inoxUser.InoxUserId)
            {
                return BadRequest();
            }

            _context.Entry(inoxUser).State = EntityState.Modified;

            try
            {
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

            return NoContent();
        }

        // POST: api/InoxUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<InoxUser>> PostInoxUser(InoxUser inoxUser)
        {
          if (_context.InoxUsers == null)
          {
              return Problem("Entity set 'AppDbContext.InoxUsers'  is null.");
          }
            _context.InoxUsers.Add(inoxUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInoxUser", new { id = inoxUser.InoxUserId }, inoxUser);
        }

        // DELETE: api/InoxUsers/5
        [HttpDelete]
        //[Route("[controller]")]
        [Route("api/[controller]/pvruserid")]
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

        [HttpGet]
        [Route("~/InoxWebApp/PvrUsers")]
        public  IActionResult SyncWithInox()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7127/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("GetPvrUsers").Result;
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
