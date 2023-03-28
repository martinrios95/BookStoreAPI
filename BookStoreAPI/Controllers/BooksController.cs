using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private Repository database;

        public BooksController(Repository database)
        {
            this.database = database;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
            return database.Books.Find(_ => true).ToList();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public ActionResult<Book> Get(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest(new { Message = "Wrong ID" });
            }

            Book book = database.Books.Find(book => book.Id == id).FirstOrDefault();

            if (book is null)
            {
                return NotFound(new { Message = "Not found" });
            }

            return book;
        }

        // POST api/<BooksController>
        [HttpPost]
        public ActionResult<Book> Post([FromBody] InsertBook insertBook)
        {
            Book book = new Book
            {

                Name = insertBook.Name,
                Price = insertBook.Price,
                Category = insertBook.Category,
                Author = insertBook.Author
            };

            database.Books.InsertOne(book);
            return book;
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] UpdateBook updateBook)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest(new { Message = "Wrong ID" });
            }

            Book oldBook = database.Books.Find(book => book.Id == id).FirstOrDefault();

            if (oldBook is null)
            {
                return NotFound(new { Message = "Not found" });
            }

            Book newBook = new Book
            {
                Id = oldBook.Id,
                Name = updateBook.Name,
                Price = updateBook.Price,
                Category = updateBook.Category,
                Author = updateBook.Author
            };

            database.Books.ReplaceOne(book => book.Id == id, newBook);

            return Ok(new { Message = "OK" });
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest(new { Message = "Wrong ID" });
            }

            Book oldBook = database.Books.Find(book => book.Id == id).FirstOrDefault();

            if (oldBook is null)
            {
                return NotFound(new { Message = "Not found" });
            }

            database.Books.DeleteOne(book => book.Id == id);

            return Ok(new { Message = "OK" });
        }
    }
}
