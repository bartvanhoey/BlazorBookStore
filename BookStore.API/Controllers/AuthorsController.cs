using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Model;
using BookStore.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDBContext _context;

        public AuthorsController(BookStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            //await Task.Delay(3000);
            return await _context.Authors.ToListAsync();
        }

        [HttpGet("GetAuthorsCount")]
        public async Task<ActionResult<ItemCount>> GetAuthorsCount()
        {
            ItemCount itemCount = new ItemCount();

            itemCount.Count = _context.Authors.Count();
            return await Task.FromResult(itemCount);
        }
        
        // GET: api/Authors
        [HttpGet("GetAuthorsByPage")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsByPage(int pageSize, int pageNumber)
        {
            //pageNumber * pageSize -> take 5
            //ItemList = Items.Skip(pageNumber * PageSize).Take(PageSize).ToList();
            
            List<Author> AuthorList = await _context.Authors.ToListAsync();
            AuthorList = AuthorList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(AuthorList);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.AuthorId }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return author;
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
