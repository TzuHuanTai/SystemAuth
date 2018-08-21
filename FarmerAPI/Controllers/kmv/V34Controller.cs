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
    [Route("api/V34")]
    public class V34Controller : Controller
    {
        private readonly KMVContext _context;

        public V34Controller(KMVContext context)
        {
            _context = context;
        }

        // GET: api/V34
        [HttpGet]
        public IEnumerable<V34> GetV34()
        {
            return _context.V34;
        }

        [HttpGet("[action]")]
        public IEnumerable<V34> GetInsideBound(decimal minLat, decimal maxLag, decimal minLng, decimal maxLng)
        {
            return _context.V34.Where(x =>
                x.V3435 >= minLat
                && x.V3435 <= maxLag
                && x.V3436 >= minLng
                && x.V3436 <= maxLng
            );
        }

        // GET: api/V34/公司代號/1
        [HttpGet("{V3401}/{V3404}")]
        public async Task<IActionResult> GetV34([FromRoute] string V3401, [FromRoute] decimal V3404)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var v34 = await _context.V34.SingleOrDefaultAsync(m => m.V3401 == V3401 && m.V3404 == V3404);

            if (v34 == null)
            {
                return NotFound();
            }

            return Ok(v34);
        }

        // PUT: api/V34/5
        [HttpPut("{V3401}/{V3404}")]
        public async Task<IActionResult> PutV34([FromRoute] string V3401, [FromRoute] decimal V3404, [FromBody] V34 v34)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (V3401 != v34.V3401 && V3404 != v34.V3404)
            {
                return BadRequest();
            }

            _context.Entry(v34).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!V34Exists(V3401, V3404))
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

        // POST: api/V34
        [HttpPost]
        public async Task<IActionResult> PostV34([FromBody] V34 v34)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.V34.Add(v34);           
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (V34Exists(v34.V3401, v34.V3404))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetV34", new { V3401 = v34.V3401, V3404 = v34.V3404 }, v34);
            return Ok(v34);
        }

        // DELETE: api/V34/5
        [HttpDelete("{V3401}/{V3404}")]
        public async Task<IActionResult> DeleteV34([FromRoute] string V3401, [FromRoute] decimal V3404)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var v34 = await _context.V34.SingleOrDefaultAsync(m => m.V3401 == V3401 && m.V3404 == V3404);
            if (v34 == null)
            {
                return NotFound();
            }

            _context.V34.Remove(v34);
            await _context.SaveChangesAsync();

            return Ok(v34);
        }

        private bool V34Exists(string V3401, decimal? V3404)
        {
            return _context.V34.Any(e => e.V3401 == V3401 && e.V3404 == V3404);
        }
    }
}