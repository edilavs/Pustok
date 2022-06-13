using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pustok.DAL;
using Pustok.Helpers;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController (AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            this._env = env;
        }
        public IActionResult Index()
        {
            var model = _context.Sliders.ToList();
            return View(model);
        }
         public IActionResult Create()
         {
             return View();
         }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (slider.ImageFile!=null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File format must be /png or /jpeg");
                   
                }

                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File Size is not correct");
                    
                }
            }
            else
            {
                ModelState.AddModelError("ImageFile", "ImageFile is required!");
            }
            if (!ModelState.IsValid)
                return View();

           

            slider.Image = FileManager.Save(_env.WebRootPath,"upload/sliders",slider.ImageFile);


            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider==null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(slider);
        }
        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (slider == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File format must be image/png or image/jpeg");
                }
                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size must be less than 2MB");
                }
                if (!ModelState.IsValid)
                    return View();


                string newFileName = FileManager.Save(_env.WebRootPath, "upload/sliders", slider.ImageFile);
                FileManager.Delete(_env.WebRootPath, "upload/sliders", existSlider.Image);
                existSlider.Image = newFileName;
            }

            existSlider.Title1 = slider.Title1;
            existSlider.Title2 = slider.Title2;
            existSlider.Text = slider.Text;
            existSlider.BtnText = slider.BtnText;
            existSlider.BtnUrl = slider.BtnUrl;
            existSlider.Order = slider.Order;

            _context.SaveChanges();
            return RedirectToAction("index");

        }

        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);

            if (slider==null)
            {
                return NotFound();
            }
            FileManager.Delete(_env.WebRootPath, "upload/sliders", slider.Image);
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return Ok();

        }
    }
}
