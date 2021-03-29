using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET Transaction/Create
        public ActionResult Create()
        {
            ViewData["Products"] = _db.Products.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.ProductId.ToString()
                });
            ViewData["Customers"] = _db.Customers.Select(c => new SelectListItem
                {
                    Text = c.FirstName + " " + c.LastName,
                    Value = c.CustomerId.ToString()
                });
            return View();
        }

        // POST: Transaction/Create
        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            transaction.DateOfTransaction = DateTimeOffset.Now;

            var createdObj = _db.Transactions.Add(transaction);

            _db.SaveChanges();

            if (_db.SaveChanges() == 1)
            {
                return RedirectToAction("Index");
                //return Redirect("/transaction/" + createdObj.TransactionId);
            }

            // ViewData["ErrMessage"]
            return View(transaction);
        }

        // GET: Transaction
        public ActionResult Index()
        {
            return View(_db.Transactions.ToArray());
        }

        public ActionResult Details(int transactionId)
        {
            var transaction = _db.Transactions.Find(transactionId);
            return View(transaction);
        }

        // GET: Transaction/Edit
        public ActionResult Edit(int transactionId)
        {
            var transaction = _db.Transactions.Find(transactionId);
            if (transaction == null)
            {
                return Redirect("Index");
            }

            ViewData["Products"] = _db.Products.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.ProductId.ToString()
                });
            ViewData["Customers"] = _db.Customers.Select(c => new SelectListItem
                {
                    Text = c.FirstName + " " + c.LastName,
                    Value = c.CustomerId.ToString()
                });
            return View(transaction);
        }

        // POST: Transaction/Edit
        [HttpPost]
        public ActionResult Edit(Transaction transaciton)
        {
            var entity = _db.Transactions.Find(transaciton.TransactionId);
            entity.CustomerId = transaciton.CustomerId;
            entity.ProductId = transaciton.ProductId;

            if (_db.SaveChanges() == 1)
            {
                return Redirect("Index");
            }

            ViewData["Products"] = _db.Products.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.ProductId.ToString()
                });
            ViewData["Customers"] = _db.Customers.Select(c => new SelectListItem
                {
                    Text = c.FirstName + " " + c.LastName,
                    Value = c.CustomerId.ToString()
                });

            return View(transaciton);
        }

        public ActionResult Delete(int transactionId)
        {
            var transaction = _db.Transactions.Find(transactionId);
            if (transaction == null)
            {
                return Redirect("Index");
            }

            return View(transaction);
        }

        [HttpPost]
        public ActionResult Delete(Transaction transaction)
        {
            var entity = _db.Transactions.Find(transaction.TransactionId);
            _db.Transactions.Remove(entity);
            if(_db.SaveChanges() == 1)
            {
                return Redirect("Index");
            }

            return View(transaction);
        }
    }
}