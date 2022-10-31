using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class ProductsController : ControllerBase
    {
        private readonly myDBContext _context;

        public ProductsController(myDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> Products([FromQuery] SearchParams searchParams)
        {
            Resoponse resoponse = new Resoponse();
            resoponse.status = true;
            resoponse.message = "Success";
            if (searchParams.categoryID!=0) { 
                int categoryID = searchParams.categoryID;
                resoponse.result = await _context.Products.Where(p => p.CategoryID == categoryID).ToListAsync();
                return new JsonResult(resoponse);
            }
            resoponse.result = await _context.Products.ToListAsync();
            return new JsonResult(resoponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> Products(int id)
        {
            Resoponse resoponse = new Resoponse();

            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                resoponse.status = false;
                resoponse.message = "Not Found";
                return new JsonResult(resoponse);
            }
            resoponse.status = true;
            resoponse.message = "Success";
            resoponse.result = products;
            return new JsonResult(resoponse);
        }

        [HttpPut("{id}")]
        public IActionResult Products(int id, Products products)
        {
            Resoponse resoponse = new Resoponse();

            if (id != products.ProductID)
            {
                resoponse.status = false;
                resoponse.message = "Bad Request";
                return new JsonResult(resoponse);

            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                 _context.SaveChangesAsync();
                resoponse.status = true;
                resoponse.message = "Success";
                resoponse.result = products;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    resoponse.status = false;
                    resoponse.message = "Bad Request";
                    return new JsonResult(resoponse);

                }
                else
                {
                    throw;
                }
            }           
            return new JsonResult(resoponse);
        }

        
        [HttpPost]
        public async Task<ActionResult<Products>> Products(Products products)
        {
            products.Category = _context.Categories.FirstOrDefault(p => p.ID == products.CategoryID);
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Products", new { id = products.ProductID }, products);
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
