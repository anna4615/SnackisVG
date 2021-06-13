using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostsAPI.Data;
using PostsAPI.Models;

namespace PostsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostImagesController : ControllerBase
    {
        private readonly SnackisContext _context;

        public PostImagesController(SnackisContext context)
        {
            _context = context;
        }

        // GET: api/PostImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostImage>>> GetPostImage()
        {
            return await _context.PostImage.ToListAsync();
        }

        // GET: api/PostImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostImage>> GetPostImage(int id)
        {
            var postImage = await _context.PostImage.FindAsync(id);

            if (postImage == null)
            {
                return NotFound();
            }

            return postImage;
        }

        // PUT: api/PostImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostImage(int id, PostImage postImage)
        {
            if (id != postImage.Id)
            {
                return BadRequest();
            }

            _context.Entry(postImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostImageExists(id))
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

        // POST: api/PostImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostImage>> PostPostImage(PostImage postImage)
        {
            _context.PostImage.Add(postImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostImage", new { id = postImage.Id }, postImage);
        }

        // DELETE: api/PostImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostImage(int id)
        {
            var postImage = await _context.PostImage.FindAsync(id);
            if (postImage == null)
            {
                return NotFound();
            }

            _context.PostImage.Remove(postImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostImageExists(int id)
        {
            return _context.PostImage.Any(e => e.Id == id);
        }
    }
}
