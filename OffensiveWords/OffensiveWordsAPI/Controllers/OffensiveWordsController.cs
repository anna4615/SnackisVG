using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OffensiveWordsAPI.Data;
using OffensiveWordsAPI.Models;

namespace OffensiveWordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffensiveWordsController : ControllerBase
    {
        private readonly SnackisContext _context;

        public OffensiveWordsController(SnackisContext context)
        {
            _context = context;
        }

        // GET: api/OffensiveWords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OffensiveWord>>> GetOffensiveWord()
        {
            return await _context.OffensiveWord.ToListAsync();
        }

        // GET: api/OffensiveWords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OffensiveWord>> GetOffensiveWord(int id)
        {
            var offensiveWord = await _context.OffensiveWord.FindAsync(id);

            if (offensiveWord == null)
            {
                return NotFound();
            }

            return offensiveWord;
        }

        // PUT: api/OffensiveWords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffensiveWord(int id, OffensiveWord offensiveWord)
        {
            if (id != offensiveWord.Id)
            {
                return BadRequest();
            }

            _context.Entry(offensiveWord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OffensiveWordExists(id))
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

        // POST: api/OffensiveWords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OffensiveWord>> PostOffensiveWord(OffensiveWord offensiveWord)
        {
            _context.OffensiveWord.Add(offensiveWord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffensiveWord", new { id = offensiveWord.Id }, offensiveWord);
        }

        // DELETE: api/OffensiveWords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffensiveWord(int id)
        {
            var offensiveWord = await _context.OffensiveWord.FindAsync(id);
            if (offensiveWord == null)
            {
                return NotFound();
            }

            _context.OffensiveWord.Remove(offensiveWord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OffensiveWordExists(int id)
        {
            return _context.OffensiveWord.Any(e => e.Id == id);
        }
    }
}
