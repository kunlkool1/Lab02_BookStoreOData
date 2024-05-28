using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.Models;

namespace ODataBookStore.Controllers
{
    public class BooksController : ODataController
    {
        private BookStoreContext _context;
        public BooksController(BookStoreContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if(context.Books.Count() == 0) {
                foreach(var b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                    context.Presses.Add(b.Press);
                }
                context.SaveChanges();
            }
        }

        [EnableQuery(PageSize =1)]
        public IActionResult Get()
        {
            return Ok(_context.Books);
        }

        [EnableQuery]
        public IActionResult Get(int key, int version)
        {
            return Ok(_context.Books.FirstOrDefault(c => c.Id == key));
        }

        [EnableQuery]
        public IActionResult Post([FromBody] Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return Created(book);
        }

        [EnableQuery]
        public IActionResult Delete([FromBody] Book book)
        {
            Book book2 = _context.Books.FirstOrDefault(c => c.Id == book.Id);
            if(book2 == null) 
            {
                return NotFound();
            }
            _context.Books.Remove(book2);
            _context.SaveChanges();
            return Ok();
        }

    }
}
