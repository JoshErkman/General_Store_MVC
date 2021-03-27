using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            return View(_db.Customers.ToList());
        }

        // GET: Customer
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // DELETE: Customer
        // Customer/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Customer customer = _db.Customers.Find(id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        // POST: Customer
        // Customer/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Customer customer = _db.Customers.Find(id);
            _db.Customers.Remove(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}