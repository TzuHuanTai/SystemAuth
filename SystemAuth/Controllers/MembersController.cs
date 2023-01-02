using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SystemAuth.ViewModels;
using SystemAuth.Models.SQLite;

namespace SystemAuth.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MembersController : Controller
    {
        private readonly SystemAuthContext _context;
        private readonly string _accessorUser;

        public MembersController(SystemAuthContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessorUser = accessor.HttpContext?.User?.FindFirst(JwtClaimTypes.Id)?.Value;
        }

		// GET: api/Members
		//[SystemAuth.Filters.AuthorizationFilter]
		//[Authorize]
		//[Authorize(Policy = "AdministratorUser")]
		//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		//[ServiceFilter(typeof(AuthorizationFilter))]
		[HttpGet]
		public IActionResult GetMember()
		{
			var result = _context.Member.Select(x => new
			{
				x.IsActive,
				x.Name,
				x.Email,
				x.AddTime,
				x.Account,
				x.UpdatedTime
			});

            if (IsAdmin())
            {
                return Ok(result);
			}
			else
			{
                return Ok(result.Where(x => x.Account == _accessorUser));
            }
		}

		// PUT: api/Members/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutMember([FromRoute] string id, [FromBody] Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if ((id != member.Account || id != _accessorUser) && !IsAdmin())
            {
                return Unauthorized();
            }

            try
            {
                Member origin = _context.Member.Single(x => x.Account == id);
                origin.Name = member.Name;
                origin.Email = member.Email;
                origin.IsActive = member.IsActive;
                origin.UpdatedTime = DateTime.Now;

				await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsMemberExists(id))
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

        // POST: api/Members
        [HttpPost]
        public async Task<IActionResult> PostMember([FromBody] Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                member.AddTime = DateTime.Now;
                member.UpdatedTime = DateTime.Now;
                _context.Member.Add(member);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IsMemberExists(member.Account))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMember", new { id = member.Account }, member);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!IsMemberExists(id))
            {
                return NotFound();
            }

            var member = _context.Member.Include(r => r.Token).Include(r => r.IMemberRole).Single(x => x.Account == id);

			if (member.Token != null)
			{
                _context.Token.Remove(member.Token);
            }
            _context.IMemberRole.RemoveRange(member.IMemberRole);
            _context.Member.Remove(member);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool IsMemberExists(string id)
        {
            return _context.Member.Any(e => e.Account == id);
        }

        private bool IsAdmin()
		{
            return _context.IMemberRole.Where(m => m.Account == _accessorUser).Any(x => x.RoleId == 1);
        }
    }
}