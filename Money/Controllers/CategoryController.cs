﻿using Microsoft.AspNetCore.Mvc;
using Money.Data;
using Money.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Money.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            IEnumerable<Category> objlist = _db.Category;
            return View(objlist);
        }

        public IActionResult Create()
        {

           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create( Category obj)

        {


            if (ModelState.IsValid)
            {
                _db.Category.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);

           
        }

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0){
                return NotFound();

            }
            var obj = _db.Category.Find(id);
            if(obj==null)
            {
                return NotFound();
            }
            return View(obj);
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

        public IActionResult Edit(Category obj)

        {


            if (ModelState.IsValid)
            {
                _db.Category.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

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


    }

}
