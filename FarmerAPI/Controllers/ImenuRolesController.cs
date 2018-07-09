using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmerAPI.Models;

namespace FarmerAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/ImenuRoles")]
    public class ImenuRolesController : Controller
    {
        private readonly WeatherContext _context;

        public ImenuRolesController(WeatherContext context)
        {
            _context = context;
        }

        // GET: api/ImenuRoles
        [HttpGet]
        public IEnumerable<ImenuRole> GetImenuRole()
        {
            return _context.ImenuRole;
        }

        // GET: api/ImenuRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImenuRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imenuRole = await _context.ImenuRole.SingleOrDefaultAsync(m => m.MenuId == id);

            if (imenuRole == null)
            {
                return NotFound();
            }

            return Ok(imenuRole);
        }

        // PUT: api/ImenuRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImenuRole([FromRoute] int id, [FromBody] ImenuRole imenuRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imenuRole.MenuId)
            {
                return BadRequest();
            }

            _context.Entry(imenuRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImenuRoleExists(id))
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

        // POST: api/ImenuRoles
        [HttpPost]
        public async Task<IActionResult> PostImenuRole([FromBody] ImenuRole imenuRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ImenuRole.Add(imenuRole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImenuRoleExists(imenuRole.MenuId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImenuRole", new { id = imenuRole.MenuId }, imenuRole);
        }

        // DELETE: api/ImenuRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImenuRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imenuRole = await _context.ImenuRole.SingleOrDefaultAsync(m => m.MenuId == id);
            if (imenuRole == null)
            {
                return NotFound();
            }

            _context.ImenuRole.Remove(imenuRole);
            await _context.SaveChangesAsync();

            return Ok(imenuRole);
        }

        private bool ImenuRoleExists(int id)
        {
            return _context.ImenuRole.Any(e => e.MenuId == id);
        }
    }
}