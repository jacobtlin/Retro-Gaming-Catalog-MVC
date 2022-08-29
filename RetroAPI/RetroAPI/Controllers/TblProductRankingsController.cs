using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroAPI.Models;

namespace RetroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblProductRankingsController : ControllerBase
    {
        private readonly RetroStoreDBContext _context;

        public TblProductRankingsController(RetroStoreDBContext context)
        {
            _context = context;
        }

        private static readonly Expression<Func<TblProductRanking, TblProductRankingDto>> AsTblProductRankingDto =
            x => new TblProductRankingDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductRanking = x.ProductRanking,
                ProductVotes = x.ProductVotes,
                ProductNameNavigation = x.ProductNameNavigation.ProductName
            };


        // GET: api/TblProductRankings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblProductRankingDto>>> GetTblProductRankings()
        {
          if (_context.TblProductRankings == null)
          {
              return NotFound();
          }
          return await _context.TblProductRankings.Include(p => p.ProductNameNavigation).Select(AsTblProductRankingDto).ToListAsync();
          //  return await _context.TblProductRankings.ToListAsync();
        }


        // GET: api/TblProductRankings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblProductRanking>> GetTblProductRanking(int id)
         {
           if (_context.TblProductRankings == null)
           {
               return NotFound();
           }
             var tblProductRanking = await _context.TblProductRankings.FindAsync(id);

             if (tblProductRanking == null)
             {
                 return NotFound();
             }

             return tblProductRanking;
         }

      
        // PUT: api/TblProductRankings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblProductRanking(int id, TblProductRankingDto productRankingDto)
        {
            if (id != productRankingDto.ProductId)
            {
                return BadRequest();
            }

            var tblProductRanking = await _context.TblProductRankings.SingleOrDefaultAsync(a => a.ProductId == id);
            if (tblProductRanking == null)
            {
                return NotFound();
            }
            var tblProductInfo = await _context.TblProductInfos.SingleOrDefaultAsync(t => t.ProductName == productRankingDto.ProductName);
            if (tblProductInfo == null)
            {
                return BadRequest();
            }

            tblProductRanking.ProductId = productRankingDto.ProductId;
            tblProductRanking.ProductName = productRankingDto.ProductName;
            tblProductRanking.ProductRanking = productRankingDto.ProductRanking;
            tblProductRanking.ProductVotes = productRankingDto.ProductVotes;

            try
            {
                _context.Entry(tblProductRanking).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblProductRankingExists(id))
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

        // POST: api/TblProductRankings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblProductRanking>> PostTblProductRanking(TblProductRanking tblProductRanking)
        {
          if (_context.TblProductRankings == null)
          {
              return Problem("Entity set 'RetroStoreDBContext.TblProductRankings'  is null.");
          }
            _context.TblProductRankings.Add(tblProductRanking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblProductRankingExists(tblProductRanking.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblProductRanking", new { id = tblProductRanking.ProductId }, tblProductRanking);
        }

        // DELETE: api/TblProductRankings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblProductRanking(int id)
        {
            if (_context.TblProductRankings == null)
            {
                return NotFound();
            }
            var tblProductRanking = await _context.TblProductRankings.FindAsync(id);
            if (tblProductRanking == null)
            {
                return NotFound();
            }

            _context.TblProductRankings.Remove(tblProductRanking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblProductRankingExists(int id)
        {
            return (_context.TblProductRankings?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
