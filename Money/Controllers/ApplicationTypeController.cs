using Microsoft.AspNetCore.Mvc;
using Money.Data;
using Money.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Money.Controllers
{
    public class ApplicationTypeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ApplicationTypeController (ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            IEnumerable<ApplicationType> objlist = _db.applicationTypes;
            return View(objlist);
        }

        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

      

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var obj = _db.applicationTypes.Find(id);
            if (obj == null)
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
            var obj = _db.applicationTypes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(ApplicationType obj)

        {


            if (ModelState.IsValid)
            {
                _db.applicationTypes.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ApplicationType obj)

        {


            if (ModelState.IsValid)
            {
                _db.applicationTypes.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create( ApplicationType obj)
        {
            _db.applicationTypes.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
