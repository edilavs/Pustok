﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            ViewBag.Tags = _context.Tags.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!_context.Authors.Any(x => x.Id == book.AuthorId))
                ModelState.AddModelError("AuthorId", "Author notfound");

            if (!_context.Genres.Any(x => x.Id == book.GenreId))
                ModelState.AddModelError("GenreId", "Genre notfound");

            CheckCreatePosterFiles(book);
            CheckCreateHoverPosterFiles(book);
            CheckImageFiles(book);
            CheckTags(book);

            if (!ModelState.IsValid)
            {
                ViewBag.Genres = _context.Genres.ToList();
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Tags = _context.Tags.ToList();


                return View();
            }

            BookImage bookPosterImage = new BookImage
            {
                Name = FileManager.Save(_env.WebRootPath, "upload/books", book.PosterFile),
                PosterStatus = true
            };

            BookImage bookHoverPosterFile = new BookImage
            {
                Name = FileManager.Save(_env.WebRootPath, "upload/books", book.HoverPosterFile),
                PosterStatus = false
            };

            book.BookImages.Add(bookPosterImage);
            book.BookImages.Add(bookHoverPosterFile);
            AddImageFiles(book, book.ImageFiles);


            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    BookTag bookTag = new BookTag
                    {
                        TagId = tagId
                    };

                    book.BookTags.Add(bookTag);
                }
            }


            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            Book book = _context.Books.Include(x=>x.BookImages).Include(x => x.BookTags).FirstOrDefault(x => x.Id == id);
           
            if (book==null)
            {
                return RedirectToAction("error", "dashboard");
            }
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            book.TagIds = book.BookTags.Select(x=>x.TagId).ToList();
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
          

            Book existBook = _context.Books.Include(x => x.BookImages).Include(x=>x.BookTags).FirstOrDefault(x => x.Id == book.Id);
            if (existBook==null)
            {
                return RedirectToAction("error", "dashboard");
            }
            if (existBook.GenreId!=book.GenreId && _context.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre is not found");
            }
            if (existBook.AuthorId != book.AuthorId && _context.Authors.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author is not found");
            }

            if (book.PosterFile!=null)
                CheckPosterFiles(book);
            if (book.HoverPosterFile != null)
                CheckHoverPosterFiles(book);
            CheckImageFiles(book);
            CheckTags(book);

            if (!ModelState.IsValid)
            {
                ViewBag.Genres = _context.Genres.ToList();
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Tags = _context.Tags.ToList();
                return View();
            }
            List<string> deletedFiles = new List<string>();
            if (book.PosterFile != null)
            {
                BookImage poster = existBook.BookImages.FirstOrDefault(x => x.PosterStatus == true);

                if (poster == null)
                {
                    poster = new BookImage { PosterStatus = true };
                    existBook.BookImages.Add(poster);
                }
                else
                    deletedFiles.Add(poster.Name);

                poster.Name = FileManager.Save(_env.WebRootPath, "upload/books", book.PosterFile);
            }

            if (book.HoverPosterFile != null)
            {
                BookImage poster = existBook.BookImages.FirstOrDefault(x => x.PosterStatus == false);

                if (poster == null)
                {
                    poster = new BookImage { PosterStatus = false };
                    existBook.BookImages.Add(poster);
                }
                else
                    deletedFiles.Add(poster.Name);

                poster.Name = FileManager.Save(_env.WebRootPath, "upload/books", book.HoverPosterFile);

            }
            existBook.BookTags.RemoveAll(bt => !book.TagIds.Contains(bt.TagId));
            foreach (var tagId in book.TagIds.Where(x => !existBook.BookTags.Any(bt => bt.TagId == x)))
            {
                BookTag bookTag = new BookTag
                {
                    TagId = tagId
                };
                existBook.BookTags.Add(bookTag);
            }
            AddImageFiles(existBook, book.ImageFiles);

            existBook.Name = book.Name;
            existBook.Rate = book.Rate;
            existBook.Desc = book.Desc;
            existBook.SubDesc = book.SubDesc;
            existBook.GenreId = book.GenreId;
            existBook.AuthorId = book.AuthorId; 
            existBook.SalePrice = book.SalePrice;
            existBook.DiscountPercent = book.DiscountPercent;
            existBook.CostPrice = book.CostPrice;
            existBook.PageSize = book.PageSize;
            existBook.IsAvailable = book.IsAvailable;

            _context.SaveChanges();

            FileManager.DeleteAll(_env.WebRootPath, "upload/books", deletedFiles);
            return RedirectToAction("index");

        }


        private void CheckImageFiles(Book book)
        {
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

                }
            }

        }
        private void CheckCreatePosterFiles(Book book)
        {
            if (book.PosterFile == null)
            {
                ModelState.AddModelError("PosterFile", "PosterFile is required");
            }
            else
            {
                CheckPosterFiles(book);
            }
        }
        private void CheckPosterFiles(Book book)
        {
                if (book.PosterFile.ContentType != "image/png" && book.PosterFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterFile", "File format must be image/png or image/jpeg");
                }

                if (book.PosterFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterFile", "File size must be less than 2MB");
                }
       
        }

        private  void CheckCreateHoverPosterFiles(Book book)
        {
            if (book.HoverPosterFile == null)
            {
                ModelState.AddModelError("HoverPosterFile", "HoverPosterFile is required");
            }
            else
            {
                CheckHoverPosterFiles(book);
            }
        }
        private void CheckHoverPosterFiles(Book book)
        {
         
                if (book.HoverPosterFile.ContentType != "image/png" && book.HoverPosterFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("HoverPosterFile", "File format must be image/png or image/jpeg");
                }

                if (book.HoverPosterFile.Length > 2097152)
                {
                    ModelState.AddModelError("HoverPosterFile", "File size must be less than 2MB");
                }
            
        }
        private void CheckTags(Book book)
        {
            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if (!_context.Tags.Any(x => x.Id == tagId))
                    {
                        ModelState.AddModelError("TagIds", "Tag id not found");
                        return;
                    }
                }
            }
        }
        private void AddImageFiles(Book book, List<IFormFile> images)
        {
            if (images != null)
            {
                foreach (var file in images)
                {
                    BookImage bookImage = new BookImage
                    {
                        Name = FileManager.Save(_env.WebRootPath, "upload/books", file),
                        PosterStatus = null
                    };

                    book.BookImages.Add(bookImage);
                }
            }
        }
    }
}
