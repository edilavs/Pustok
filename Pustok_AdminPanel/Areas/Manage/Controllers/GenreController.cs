

using Microsoft.AspNetCore.Mvc;
using Pustok.DAL;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class GenreController : Controller
    {
        private readonly AppDbContext _context;
        public GenreController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var genre = _context.Genres.ToList();
            return View(genre);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {

            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(genre);
        }
        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Genre existGenre = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);
            if (existGenre == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            existGenre.Name = genre.Name;
         

            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult Delete(int id)
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(genre);
        }
        [HttpPost]
        public IActionResult Delete(Genre genre)
        {
            Genre existGenre = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);
            if (existGenre == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            _context.Genres.Remove(existGenre);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        //public IActionResult SweetDelete(int id)
        //{
        //    Genre genre = _context.Genres.FirstOrDefault(x=>x.Id==id);
        //    if (genre==null)
        //    {
        //        return RedirectToAction("error", "dashboard");
        //    }

        //    //_context.Genres.Remove(genre);
        //    //_context.SaveChanges();
        //    return Ok();


        //}

    }
}
