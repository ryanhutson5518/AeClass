using EComm.DataAccess;
using EComm.Model;
using EComm.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Web.Controllers
{
    public class ProductController : Controller
    {
        private ECommDataContext _db { get; set; }

        public ProductController(ECommDataContext db)
        {
            _db = db;
        }

        [Authorize(Policy ="AdminsOnly")]
        [Route("product/{id:int}")]
        public IActionResult Details(int id)
        {
            var product = _db.Products.Include(p => p.Supplier).SingleOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _db.Products.Include(p => p.Supplier).SingleOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            var suppliers = await _db.Suppliers.ToListAsync();
            var pvm = new ProductEditViewModel
            {
                Product = new Product
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                    Package = product.Package,
                    IsDiscontinued = product.IsDiscontinued,
                    SupplierId = product.SupplierId
                },

                Suppliers = suppliers.Select(sli => new SelectListItem
                {
                    Text = sli.CompanyName, Value = sli.Id.ToString()
                }).ToList()
            };

            return View(pvm);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel pvm)
        {
            if (!ModelState.IsValid)
            {
                var suppliers = _db.Suppliers.ToList();
                pvm.Suppliers = suppliers.Select(sli => new SelectListItem
                {
                    Text = sli.CompanyName,
                    Value = sli.Id.ToString()
                }).ToList();

                return View(pvm);
            }

            var product = new Product
            {
                Id = pvm.Product.Id,
                ProductName = pvm.Product.ProductName,
                UnitPrice = pvm.Product.UnitPrice,
                Package = pvm.Product.Package,
                IsDiscontinued = pvm.Product.IsDiscontinued,
                SupplierId = pvm.Product.SupplierId
            };

            _db.Attach(product);
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("api/products")]
        public IActionResult Get()
        {
            var products = _db.Products.ToList();
            return new ObjectResult(products);
        }

        [HttpGet("api/product/{id:int}")]
        public IActionResult Get(int id)
        {
            var product = _db.Products.SingleOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            return new ObjectResult(product);
        }

        [HttpPut("api/product/{id:int}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (product == null || product.Id != id) return BadRequest();

            var existing = _db.Products.SingleOrDefault(p => p.Id == id);

            if (existing == null) return NotFound();

            existing.ProductName = product.ProductName;
            existing.UnitPrice = product.UnitPrice;
            existing.Package = product.Package;
            existing.IsDiscontinued = product.IsDiscontinued;
            existing.SupplierId = product.SupplierId;

            _db.SaveChanges();

            return new NoContentResult();
        }
    }
}
