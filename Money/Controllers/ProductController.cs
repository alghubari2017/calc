using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Money.Data;
using Money.Models;
using Money.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Money.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db , IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(prodcutVM prodcutVM)
        {
            var file = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (prodcutVM.Prodcut.Id == 0)
            {
                //creating
                string upload = webRootPath + Wc.ImagePath;
                string fileName = Guid.NewGuid().ToString();
               
                string extention = Path.GetExtension(file[0].FileName);
                string fi = extention;
                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.CreateNew))
                {
                    
                    file[0].CopyTo(fileStream);
                }

                prodcutVM.Prodcut.Image = fileName + extention;
                _db.Product.Add(prodcutVM.Prodcut);
            }
           
            else
            {
                //updating
                var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(u => u.Id == prodcutVM.Prodcut.Id);
                if (file.Count > 0)
                {
                    string upload = webRootPath + Wc.ImagePath;
                    string fileName = Guid.NewGuid().ToString();

                    string extention = Path.GetExtension(file[0].FileName);


                    var oldFile = Path.Combine(upload, objFromDb.Image);
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.CreateNew))
                        {

                            file[0].CopyTo(fileStream);
                        }
                        prodcutVM.Prodcut.Image = fileName + extention;
                    }
                }
                else
                {
                    prodcutVM.Prodcut.Image = objFromDb.Image;
                }
                    
                    


                
            }
            _db.Product.Update(prodcutVM.Prodcut);
            _db.SaveChanges();
            return RedirectToAction("Index");

           
            return View();
        }

      

       
        public IActionResult Upsert( int ? id)

        {
            //IEnumerable<SelectListItem> CategoryDropDwon = _db.Category.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //}
            //) ;
            //// ViewBag.CategoryDropDwon = CategoryDropDwon;
            //ViewData["CategoryDropDwon" ]= CategoryDropDwon;
            //Product product = new Product();

            prodcutVM productVM = new prodcutVM()
            {
                Prodcut = new Product(),
                CategorySelectListItem = _db.Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null)
            {
                // this is for create
                return View(productVM);
            }
            else
            {
                productVM.Prodcut = _db.Product.Find(id);
                if (productVM.Prodcut == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }



           
        }

    
        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

   



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)

        {


            if (ModelState.IsValid)
            {
                _db.Category.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);


        }
        public IActionResult Index()
        {

            IEnumerable<Product> objlist = _db.Product;
            return View(objlist);
        }


    }

}
