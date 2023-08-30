using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TamansShop.Data;
using TamansShop.Models;

namespace TamansShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly TamansShopContext _context;
        private readonly ILogger<ProductController> _logger;


        public ProductController(TamansShopContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,ImagePath")] Product product)
        {
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            product.UserId = currentUserId;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }
        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // Check if the current user is the owner of the product
            if (product.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return Forbid(); // Return a forbidden status
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,ImagePath")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            // Check if the current user is the owner of the product
            if (product.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return Forbid(); // Return a forbidden status
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            // Check if the current user is the owner of the product
            if (product.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return Forbid(); // Return a forbidden status
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'TamansShopContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            // Check if the current user is the owner of the product
            if (product.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return Forbid(); // Return a forbidden status
            }
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
