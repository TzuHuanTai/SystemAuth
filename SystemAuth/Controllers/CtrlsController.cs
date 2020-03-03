using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemAuth.Models.SQLite;

namespace SystemAuth.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CtrlsController : Controller
    {
        private readonly SystemAuthContext _context;

        public CtrlsController(SystemAuthContext context)
        {
            _context = context;
        }

        // GET: api/Ctrls
        [HttpGet]
        public IEnumerable<Ctrl> GetCtrl()
        {
            return _context.Ctrl;
        }

        // GET: api/Ctrls/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCtrl([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ctrl = await _context.Ctrl.SingleOrDefaultAsync(m => m.CtrlId == id);

            if (ctrl == null)
            {
                return NotFound();
            }

            return Ok(ctrl);
        }

        // PUT: api/Ctrls/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCtrl([FromRoute] int id, [FromBody] Ctrl ctrl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ctrl.CtrlId)
            {
                return BadRequest();
            }

            _context.Entry(ctrl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CtrlExists(id))
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

        // POST: api/Ctrls
        [HttpPost]
        public async Task<IActionResult> PostCtrl([FromBody] Ctrl ctrl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ctrl.Add(ctrl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCtrl", new { id = ctrl.CtrlId }, ctrl);
        }

        // DELETE: api/Ctrls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCtrl([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ctrl = await _context.Ctrl.SingleOrDefaultAsync(m => m.CtrlId == id);
            if (ctrl == null)
            {
                return NotFound();
            }

            _context.Ctrl.Remove(ctrl);
            await _context.SaveChangesAsync();

            return Ok(ctrl);
        }

        private bool CtrlExists(int id)
        {
            return _context.Ctrl.Any(e => e.CtrlId == id);
        }
    }
}