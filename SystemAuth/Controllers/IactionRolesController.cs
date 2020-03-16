using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SystemAuth.Models.SQLite;

namespace SystemAuth.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class IActionRolesController : Controller
    {
        private readonly SystemAuthContext _context;

        public IActionRolesController(SystemAuthContext context)
        {
            _context = context;
        }

        // GET: api/IactionRoles
        [HttpGet]
        public IEnumerable<IActionRole> GetIactionRole()
        {
            return _context.IActionRole;
        }

        // GET: api/IactionRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIactionRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var iactionRole = await _context.IActionRole.SingleOrDefaultAsync(m => m.ActionId == id);

            if (iactionRole == null)
            {
                return NotFound();
            }

            return Ok(iactionRole);
        }

        // POST: api/IactionRoles
        [HttpPost]
        public async Task<IActionResult> PostIactionRole([FromBody] IActionRole iActionRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.IActionRole.Add(iActionRole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IactionRoleExists(iActionRole.ActionId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIactionRole", new { id = iActionRole.ActionId }, iActionRole);
        }

        // DELETE: api/IactionRoles/5
        [HttpDelete("{ActionId}/{RoleId}")]
        public async Task<IActionResult> DeleteIactionRole([FromRoute] int ActionId, [FromRoute] int RoleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //IActionRole是雙主鍵！兩個pk都要判斷！
            var iactionRole = await _context.IActionRole.SingleOrDefaultAsync(m => m.ActionId == ActionId && m.RoleId == RoleId);
            if (iactionRole == null)
            {
                return NotFound();
            }

            _context.IActionRole.Remove(iactionRole);
            await _context.SaveChangesAsync();

            return Ok(iactionRole);
        }

        private bool IactionRoleExists(int id)
        {
            return _context.IActionRole.Any(e => e.ActionId == id);
        }
    }
}