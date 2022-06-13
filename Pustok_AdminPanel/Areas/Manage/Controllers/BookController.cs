using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Helpers;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController (AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            this._env = env;
        }
        public IActionResult Index()
        {
            var book = _context.Books.Include(x=>x.Author).Include(x=>x.Genre).ToList();
            return View(book);
        }

        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                return View();
            }


            if (!_context.Authors.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author notfound");
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();

                return View();
            }

            if (!_context.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre notfound");
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();

                return View();
            }

            if (book.PosterFile == null)
            {
                ModelState.AddModelError("PosterFile", "PosterFile is required");
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                return View();
            }
            else
            {
                if (book.PosterFile.ContentType != "image/png" && book.PosterFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterFile", "File format must be image/png or image/jpeg");
                }

                if (book.PosterFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterFile", "File size must be less than 2MB");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Authors = _context.Authors.ToList();
                    ViewBag.Genres = _context.Genres.ToList();
                    return View();
                }

                BookImage bookImage = new BookImage
                {
                    Name = FileManager.Save(_env.WebRootPath, "upload/books", book.PosterFile),
                    PosterStatus = true
                };

                book.BookImages.Add(bookImage);
            }



            if (book.ImageFiles != null)
            {
                foreach (var file in book.ImageFiles)
                {
                    if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFiles", "File format must be image/png or image/jpeg");
                    }

                    if (file.Length > 2097152)
                    {
                        ModelState.AddModelError("ImageFiles", "File size must be less than 2MB");
                    }

                    if (!ModelState.IsValid)
                    {
                        ViewBag.Authors = _context.Authors.ToList();
                        ViewBag.Genres = _context.Genres.ToList();
                        return View();
                    }
                }

                foreach (var file in book.ImageFiles)
                {
                    BookImage bookImage = new BookImage
                    {
                        Name = FileManager.Save(_env.WebRootPath, "upload/books", file),
                        PosterStatus = null
                    };

                    book.BookImages.Add(bookImage);
                }
            }



            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            Book book = _context.Books.FirstOrDefault(x => x.Id == id);
            ViewBag.Authors = _context.Authors.ToList();
            if (book==null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(book);
        }
    }
}
