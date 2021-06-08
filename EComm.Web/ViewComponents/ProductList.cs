using EComm.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Web.ViewComponents
{
    public class ProductList : ViewComponent
    {
        private ECommDataContext _db { get; set; }

        public ProductList(ECommDataContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _db.Products.Include(p => p.Supplier).ToListAsync();
            return View(products);
        }
    }
}
