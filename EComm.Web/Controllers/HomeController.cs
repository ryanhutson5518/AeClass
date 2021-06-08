using EComm.DataAccess;
using EComm.Model;
using EComm.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Web.Controllers
{
    public class HomeController : Controller
    {
        private ECommDataContext _db { get; set; }

        public HomeController(ECommDataContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> Index()   // This code was here until I used a ViewComponent
        //{
        //    var products = await _db.Products.Include(p => p.Supplier).ToListAsync();

        //    return View(products);
        //}

        //public List<Product> Index()   // Returns JSON data
        //{
        //    return _db.Products.ToList();
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
