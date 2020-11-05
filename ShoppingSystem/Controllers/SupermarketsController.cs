using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingSystem.Data;
using ShoppingSystem.Models;
using ShoppingSystem.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSystem.Controllers
{
    public class SupermarketsController : Controller
    {
        private readonly ShoppingContext context;

        public SupermarketsController(ShoppingContext context)
        {
            this.context = context;
        }

        // GET: Supermarkets
        public async Task<IActionResult> Index(int? pageNumber, int pageSize = 3)
        {
            ViewBag.PageSize = pageSize;
            var supermarkets = from s in context.Supermarkets select s;
            return View(await PaginationService<Supermarket>.CreateAsync(supermarkets.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Supermarkets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supermarket = await context.Supermarkets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supermarket == null)
            {
                return NotFound();
            }

            return View(supermarket);
        }

        // GET: Supermarkets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supermarkets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address")] Supermarket supermarket)
        {
            if (ModelState.IsValid)
            {
                context.Add(supermarket);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supermarket);
        }

        // GET: Supermarkets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supermarket = await context.Supermarkets.FindAsync(id);
            if (supermarket == null)
            {
                return NotFound();
            }
            return View(supermarket);
        }

        // POST: Supermarkets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address")] Supermarket supermarket)
        {
            if (id != supermarket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(supermarket);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupermarketExists(supermarket.Id))
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
            return View(supermarket);
        }

        // GET: Supermarkets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supermarket = await context.Supermarkets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supermarket == null)
            {
                return NotFound();
            }

            return View(supermarket);
        }

        // POST: Supermarkets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supermarket = await context.Supermarkets.FindAsync(id);
            context.Supermarkets.Remove(supermarket);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupermarketExists(int id)
        {
            return context.Supermarkets.Any(e => e.Id == id);
        }
    }
}
