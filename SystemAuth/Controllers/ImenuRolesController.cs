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
    public class ImenuRolesController : Controller
    {
        private readonly SystemAuthContext _context;

        public ImenuRolesController(SystemAuthContext context)
        {
            _context = context;
        }                

        // GET: api/ImenuRoles
        [HttpGet]
        public IEnumerable<IMenuRole> GetImenuRole()
        {
            return _context.IMenuRole;
        }

        // GET: api/ImenuRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImenuRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imenuRole = await _context.IMenuRole.SingleOrDefaultAsync(m => m.MenuId == id);

            if (imenuRole == null)
            {
                return NotFound();
            }

            return Ok(imenuRole);
        }

        // POST: api/ImenuRoles
        [HttpPost]
        public async Task<IActionResult> PostImenuRole([FromBody] IMenuRole iMenuRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.IMenuRole.Add(iMenuRole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImenuRoleExists(iMenuRole.MenuId, iMenuRole.RoleId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImenuRole", new { id = iMenuRole.MenuId }, iMenuRole);
        }

        // DELETE: api/ImenuRoles/5
        [HttpDelete("{MenuId}/{RoleId}")]
        public async Task<IActionResult> DeleteImenuRole([FromRoute] int MenuId, [FromRoute] int RoleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //ImenuRole是雙主鍵！兩個pk都要判斷！
            var iMenuRole = await _context.IMenuRole.SingleOrDefaultAsync(m => m.MenuId == MenuId && m.RoleId == RoleId);
            if (iMenuRole == null)
            {
                return NotFound();
            }

            _context.IMenuRole.Remove(iMenuRole);
            await _context.SaveChangesAsync();

            return Ok(iMenuRole);
        }

        private bool ImenuRoleExists(int MenuId, int RoleId)
        {
            //ImenuRole是雙主鍵！兩個pk都要判斷！
            return _context.IMenuRole.Any(e => e.MenuId == MenuId && e.RoleId == RoleId);
        }
    }
}