using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Models;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetBookModal(int id)
        {
            Book book = _context.Books.Include(x => x.Genre).Include(x => x.Author).Include(x => x.BookImages).FirstOrDefault(x => x.Id == id);

            if (book == null)
                return NotFound();

            return PartialView("_BookModalPartial", book);
        }
        public IActionResult Detail(int id)
        {
            Book book = _context.Books
                .Include(x=>x.BookImages)
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookTags).ThenInclude(x=>x.Tag)
                .FirstOrDefault(x => x.Id == id);
            if (book==null)
            {
                return RedirectToAction("error", "dashboard");
            }
            BookDetailViewModel bookVM = new BookDetailViewModel
            {
                Book = book,
                RelatedBooks = _context.Books.Include(x => x.BookImages)
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookTags).ThenInclude(x => x.Tag).Where(x => x.GenreId == book.GenreId).Take(6).ToList()
            };
            return View(bookVM);
        }
    }
}
