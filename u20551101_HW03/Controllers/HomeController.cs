using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u20551101_HW03.Models;
using System.Text;
using System.IO;


namespace u20551101_HW03.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        public ActionResult Index(int studentpage = 1, int bookpage = 1)
        {
            int pageSize = 10;
            int studentSkipCount = (studentpage - 1) * pageSize;
            int bookSkipCount = (bookpage - 1) * pageSize;

            var students = db.students.OrderBy(s => s.studentId).Skip(studentSkipCount).Take(pageSize).ToList();
            var books = db.books.OrderBy(b => b.bookId).Skip(bookSkipCount).Take(pageSize).ToList();

            int studentTotalCount = db.students.Count();
            int bookTotalCount = db.books.Count();
            int studentTotalPages = (int)Math.Ceiling((double)studentTotalCount / pageSize);
            int bookTotalPages = (int)Math.Ceiling((double)bookTotalCount / pageSize);

            ViewBag.Books = books.Select(b => new SelectListItem
            {
                Value = b.authorId.ToString(),
                Text = b.authors.name
            }).ToList();

            ViewBag.Books = books.Select(b => new SelectListItem
            {
                Value = b.typeId.ToString(),
                Text = b.types.name
            }).ToList();



            ViewBag.Books = books;
            ViewBag.Students = students;
            ViewBag.StudentCurrentPage = studentpage;
            ViewBag.BookCurrentPage = bookpage;
            ViewBag.StudentTotalPages = studentTotalPages;
            ViewBag.BookTotalPages = bookTotalPages;
            return View();
        }

        public ActionResult Maintain(int authorpage = 1, int typepage = 1, int borrowpage = 1)
        {
            int pageSize = 10;

            int authorSkipCount = (authorpage - 1) * pageSize;
            int typeSkipCount = (typepage - 1) * pageSize;
            int borrowSkipCount = (borrowpage - 1) * pageSize;

            int authorTotalCount = db.authors.Count();
            int typeTotalCount = db.types.Count();
            int borrowTotalCount = db.borrows.Count();

            var authors = db.authors.OrderBy(s => s.authorId).Skip(authorSkipCount).Take(pageSize).ToList();
            var types = db.types.OrderBy(s => s.typeId).Skip(typeSkipCount).Take(pageSize).ToList();
            var borrows = db.borrows.OrderBy(b => b.borrowId).Skip(borrowSkipCount).Take(pageSize).ToList();


            int authorTotalPages = (int)Math.Ceiling((double)authorTotalCount / pageSize);
            int typeTotalPages = (int)Math.Ceiling((double)typeTotalCount / pageSize);
            int borrowTotalPages = (int)Math.Ceiling((double)borrowTotalCount / pageSize);




            ViewBag.Borrows = borrows.Select(b => new SelectListItem
            {
                Value = b.studentId.ToString(),
                Text = b.students.name + " " + b.students.surname
            }).ToList();

            ViewBag.Borrows = borrows.Select(b => new SelectListItem
            {
                Value = b.bookId.ToString(),
                Text = b.books.name
            }).ToList();

            ViewBag.Borrows = borrows;
            ViewBag.Authors = authors;
            ViewBag.Types = types;

            ViewBag.AuthorCurrentPage = authorpage;
            ViewBag.TypeCurrentPage = typepage;
            ViewBag.BorrowCurrentPage = borrowpage;

            ViewBag.AuthorTotalPages = authorTotalPages;
            ViewBag.TypeTotalPages = typeTotalPages;
            ViewBag.BorrowTotalPages = borrowTotalPages;

            return View();
        }

        public ActionResult Report()
        {
            var bookCounts = db.borrows
                .Where(b => b.takenDate.HasValue && b.takenDate.Value.Year >= 2015 && b.takenDate.Value.Year <= 2017)
                .GroupBy(b => new
                {
                    Year = b.takenDate.Value.Year,
                    BookName = b.books.name
                })
                .Select(group => new
                {
                    Year = group.Key.Year,
                    BookName = group.Key.BookName,
                    BorrowCount = group.Count()
                })
                .ToList();

            var labels = bookCounts.Select(b => b.BookName).Distinct().ToArray();
            var year2015Data = bookCounts.Where(b => b.Year == 2015).Select(b => b.BorrowCount).ToArray();
            var year2016Data = bookCounts.Where(b => b.Year == 2016).Select(b => b.BorrowCount).ToArray();
            var year2017Data = bookCounts.Where(b => b.Year == 2017).Select(b => b.BorrowCount).ToArray();

            ViewBag.ChartLabels = labels;
            ViewBag.Year2015Data = year2015Data;
            ViewBag.Year2016Data = year2016Data;
            ViewBag.Year2017Data = year2017Data;

            return View();
        }
     

    }
}