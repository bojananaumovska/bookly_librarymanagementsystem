using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library_Managment_Project.Models;
using Microsoft.AspNet.Identity;

namespace Library_Managment_Project.Controllers
{
    public class LoansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Loans
        public ActionResult Index()
        {
            var loans = db.Loans
                .Include(l => l.BookCopy)
                .Include(l => l.BookCopy.Book)
                .Include(l => l.User)
                .ToList();

            foreach (var loan in loans)
            {
                if (loan.Status == Loan.LoanStatus.Active && loan.DueDate < DateTime.Now)
                {
                    loan.Status = Loan.LoanStatus.Overdue;
                }
            }

            db.SaveChanges();

            if (!User.IsInRole("Admin") && !User.IsInRole("Librarian"))
            {
                var currentUserId = User.Identity.GetUserId();
                loans = loans.Where(l => l.UserId == currentUserId).ToList();
            }

            return View(loans);
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Create()
        {
            var availableBookCopies = db.BookCopies
                                        .Include(bc => bc.Book)
                                        .Where(bc => !bc.Loans.Any(l => l.Status != Loan.LoanStatus.Returned))
                                        .ToList();

            ViewBag.BookCopyId = new SelectList(
                availableBookCopies.Select(bc => new {
                    Id = bc.Id,
                    Display = $"{bc.InventoryNumber} - {bc.Book.Title}"
                }),
                "Id",
                "Display"
            );

            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Create([Bind(Include = "Id,UserId,BookCopyId,LoanDate,DueDate,ReturnDate,Status")] Loan loan)
        {
            var bookCopy = db.BookCopies
                             .Include(bc => bc.Loans)
                             .FirstOrDefault(bc => bc.Id == loan.BookCopyId);

            if (bookCopy == null)
            {
                ModelState.AddModelError("", "Book copy not found.");
            }

            if (ModelState.IsValid)
            {
                loan.Status = Loan.LoanStatus.Active;
                db.Loans.Add(loan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var availableBookCopies = db.BookCopies
                                        .Include(bc => bc.Book)
                                        .Where(bc => !bc.Loans.Any(l => l.Status != Loan.LoanStatus.Returned))
                                        .ToList();

            ViewBag.BookCopyId = new SelectList(
                availableBookCopies.Select(bc => new {
                    Id = bc.Id,
                    Display = $"{bc.InventoryNumber} - {bc.Book.Title}"
                }),
                "Id",
                "Display",
                loan.BookCopyId
            );

            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", loan.UserId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Loan loan = db.Loans
                          .Include(l => l.BookCopy)
                          .Include(l => l.BookCopy.Book)
                          .FirstOrDefault(l => l.Id == id);

            if (loan == null)
                return HttpNotFound();

            // Only show copies that are available OR the one currently assigned to this loan
            var bookCopies = db.BookCopies
                               .Include(bc => bc.Book)
                               .Where(bc => !bc.Loans.Any(l => l.Status != Loan.LoanStatus.Returned)
                                            || bc.Id == loan.BookCopyId)
                               .ToList();

            ViewBag.BookCopyId = new SelectList(
                bookCopies.Select(bc => new {
                    Id = bc.Id,
                    Display = $"{bc.InventoryNumber} - {bc.Book.Title}"
                }),
                "Id",
                "Display",
                loan.BookCopyId
            );

            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", loan.UserId);

            // The loan object already has the dates (LoanDate, DueDate, ReturnDate) pre-filled
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Edit([Bind(Include = "Id,UserId,BookCopyId,LoanDate,DueDate,ReturnDate,Status")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookCopyId = new SelectList(db.BookCopies, "Id", "InventoryNumber", loan.BookCopyId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", loan.UserId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
