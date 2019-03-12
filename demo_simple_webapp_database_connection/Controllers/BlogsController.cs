using demo_simple_webapp_database_connection.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace demo_simple_webapp_database_connection.Controllers
{
    public class BlogsController : Controller
    {
        private WebAppContext _context;

        public BlogsController(WebAppContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Blogs.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Blogs.Add(blog);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

    }
}