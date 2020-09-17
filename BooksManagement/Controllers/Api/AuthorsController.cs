using BooksManagement.Context;
using BooksManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagement.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get()
        {
            return _context.Authors.ToList();
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public ActionResult<Author> Get(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            return author;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Authors.Add(author);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetAuthor", new { id = author.Id }, author);
        }
    }
}
