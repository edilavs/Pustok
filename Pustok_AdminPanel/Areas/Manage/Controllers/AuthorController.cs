using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;
        public AuthorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Authors.Include(x=>x.Books).ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Authors.Add(author);
            _context.SaveChanges();

            return RedirectToAction("index");
        }



        public IActionResult Edit(int id)
        {
           
            Author author = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(author);
        }
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Author existAuthor = _context.Authors.FirstOrDefault(x => x.Id == author.Id);
            if (existAuthor==null)
            {
                 return RedirectToAction("error", "dashboard");
            }

            existAuthor.FullName = author.FullName;
            existAuthor.BirthDate = author.BirthDate;

            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult Delete(int id)
        {
            Author author = _context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id);
            if (author==null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(author);
        }


        [HttpPost]
        public IActionResult Delete(Author author)
        {
            Author existAuthor = _context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == author.Id);

            if (existAuthor==null)
            {
                return RedirectToAction("error", "dashboard");
            }
            _context.Authors.Remove(existAuthor);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        
    }
}
