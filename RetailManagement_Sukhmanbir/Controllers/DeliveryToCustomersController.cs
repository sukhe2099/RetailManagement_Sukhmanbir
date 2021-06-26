using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetailManagement_Sukhmanbir.Data;
using RetailManagement_Sukhmanbir.Models;

namespace RetailManagement_Sukhmanbir.Controllers
{
    [Authorize]
    public class DeliveryToCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryToCustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryToCustomers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeliveryToCustomers.Include(d => d.Customer).Include(d => d.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeliveryToCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryToCustomer = await _context.DeliveryToCustomers
                .Include(d => d.Customer)
                .Include(d => d.Supplier)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (deliveryToCustomer == null)
            {
                return NotFound();
            }

            return View(deliveryToCustomer);
        }

        // GET: DeliveryToCustomers/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Name");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name");
            return View();
        }

        // POST: DeliveryToCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SupplierID,CustomerID,DeliveryDate,Price")] DeliveryToCustomer deliveryToCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryToCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Name", deliveryToCustomer.CustomerID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name", deliveryToCustomer.SupplierID);
            return View(deliveryToCustomer);
        }

        // GET: DeliveryToCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryToCustomer = await _context.DeliveryToCustomers.FindAsync(id);
            if (deliveryToCustomer == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Name", deliveryToCustomer.CustomerID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name", deliveryToCustomer.SupplierID);
            return View(deliveryToCustomer);
        }

        // POST: DeliveryToCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SupplierID,CustomerID,DeliveryDate,Price")] DeliveryToCustomer deliveryToCustomer)
        {
            if (id != deliveryToCustomer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryToCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryToCustomerExists(deliveryToCustomer.ID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Name", deliveryToCustomer.CustomerID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name", deliveryToCustomer.SupplierID);
            return View(deliveryToCustomer);
        }

        // GET: DeliveryToCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryToCustomer = await _context.DeliveryToCustomers
                .Include(d => d.Customer)
                .Include(d => d.Supplier)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (deliveryToCustomer == null)
            {
                return NotFound();
            }

            return View(deliveryToCustomer);
        }

        // POST: DeliveryToCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryToCustomer = await _context.DeliveryToCustomers.FindAsync(id);
            _context.DeliveryToCustomers.Remove(deliveryToCustomer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryToCustomerExists(int id)
        {
            return _context.DeliveryToCustomers.Any(e => e.ID == id);
        }
    }
}
