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
    public class IMemberRolesController : Controller
    {
        private readonly SystemAuthContext _context;

        public IMemberRolesController(SystemAuthContext context)
        {
            _context = context;
        }

        // GET: api/ImemRoles
        [HttpGet]
        public IEnumerable<IMemberRole> GetImemRole()
        {
            return _context.IMemberRole;
        }

        // GET: api/ImemRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImemRole([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imemRole = await _context.IMemberRole.Where(m => m.Account == id).ToListAsync();

            if (imemRole == null)
            {
                return NotFound();
            }

            return Ok(imemRole);
        }

        // POST: api/ImemRoles
        [HttpPost]
        public async Task<IActionResult> PostImemRole([FromBody] IMemberRole iMemberRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.IMemberRole.Add(iMemberRole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImemRoleExists(iMemberRole.Account))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetImemRole", new { id = iMemberRole.Account }, iMemberRole);
        }

        // DELETE: api/ImemRoles/5
        [HttpDelete("{Account}/{RoleId}")]
        public async Task<IActionResult> DeleteImemRole([FromRoute] string Account, [FromRoute] int RoleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //ImemRole是雙主鍵！兩個pk都要判斷！
            var iMemberRole = await _context.IMemberRole.SingleOrDefaultAsync(m => m.Account == Account && m.RoleId == RoleId);
            if (iMemberRole == null)
            {
                return NotFound();
            }

            _context.IMemberRole.Remove(iMemberRole);
            await _context.SaveChangesAsync();

            return Ok(iMemberRole);
        }

        private bool ImemRoleExists(string id)
        {
            return _context.IMemberRole.Any(e => e.Account == id);
        }
    }
}