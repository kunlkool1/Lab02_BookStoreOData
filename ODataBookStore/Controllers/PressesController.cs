using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataBookStore.Models;

namespace ODataBookStore.Controllers
{
/*    [Route("api/[controller]")]
    [ApiController]*/
    public class PressesController : ODataController
    {
        private BookStoreContext _context;
        public PressesController(BookStoreContext context)
        {
            _context = context;
            if(context.Books.Count() == 0)
            {
                foreach(var b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                    context.Presses.Add(b.Press);
                }
                context.SaveChanges();
            }
        }
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Presses);
        }

    }
}
