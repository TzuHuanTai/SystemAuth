using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SystemAuth.Models;
using SystemAuth.ViewModels;

namespace SystemAuth.Controllers
{
    [Produces("application/json")]
    [Route("api/Members")]
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
		public IEnumerable<Member> GetMember()
		{
			List<Member> result = _context.Member.ToList();
			
			result.Select(x => {
				x.Password = ""; return x;
			}).ToList();

			return result;
		}

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = await _context.Member.SingleOrDefaultAsync(m => m.Account == id);
			//不回傳密碼
			member.Password = "";

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

		// GET: api/Members/GetMemberName/aaa
		[HttpGet("[action]/{account}")]
		public IActionResult GetMemberName([FromRoute] string account)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var member = _context.Member.Where(m => m.Account == account).ToList()
				.Select(x => 
					new { x.Account, x.FirstName, x.LastName}
				);						


			if (member == null)
			{
				return NotFound();
			}

			return Ok(member.FirstOrDefault());
		}

		// PUT: api/Members/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutMember([FromRoute] string id, [FromBody] Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != member.Account)
            {
                return BadRequest();
            }

			Member Origin = _context.Member.Single(x => x.Account == id);

			_context.Entry(member).State = EntityState.Modified;

            try
            {
				//防止新增時間被更動
				member.AddTime = Origin.AddTime;

				//刷新修改時間
                member.UpdatedTime = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

            //新增、更新時間以伺服器時間為準
            member.AddTime = DateTime.Now;
            member.UpdatedTime = DateTime.Now;

            _context.Member.Add(member);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemberExists(member.Account))
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

            var member = await _context.Member.SingleOrDefaultAsync(m => m.Account == id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Member.Remove(member);
            await _context.SaveChangesAsync();

            return Ok(member);
        }

        private bool MemberExists(string id)
        {
            return _context.Member.Any(e => e.Account == id);
        }
    }
}