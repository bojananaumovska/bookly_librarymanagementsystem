using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library_Managment_Project.Models;

namespace Library_Managment_Project.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.Category);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = db.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(
                db.Authors
                    .OrderBy(a => a.LastName)
                    .Select(a => new
                    {
                        Id = a.Id,
                        FullName = a.FirstName + " " + a.LastName
                    }),
                "Id",
                "FullName"
            );
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Book book, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Images/Books/"), fileName);

                    if (!Directory.Exists(Server.MapPath("~/Content/Images/Books/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Images/Books/"));
                    }

                    ImageFile.SaveAs(path);
                    book.ImagePath = "/Content/Images/Books/" + fileName;
                }

                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(
                db.Authors
                    .OrderBy(a => a.LastName)
                    .Select(a => new
                    {
                        Id = a.Id,
                        FullName = a.FirstName + " " + a.LastName
                    }),
                "Id",
                "FullName",
                book.AuthorId 
            );
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Book book, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                var bookInDb = db.Books.Find(book.Id);
                if (bookInDb == null)
                {
                    return HttpNotFound();
                }

                bookInDb.Title = book.Title;
                bookInDb.ISBN = book.ISBN;
                bookInDb.PublishedYear = book.PublishedYear;
                bookInDb.Publisher = book.Publisher;
                bookInDb.Description = book.Description;
                bookInDb.CategoryId = book.CategoryId;
                bookInDb.AuthorId = book.AuthorId;

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(ImageFile.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Books/"), fileName);

                    if (!System.IO.Directory.Exists(Server.MapPath("~/Content/Images/Books/")))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Content/Images/Books/"));
                    }

                    ImageFile.SaveAs(path);
                    bookInDb.ImagePath = "/Content/Images/Books/" + fileName;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = db.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            if (!string.IsNullOrEmpty(book.ImagePath))
            {
                var fullPath = Server.MapPath(book.ImagePath);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            db.Books.Remove(book);
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
