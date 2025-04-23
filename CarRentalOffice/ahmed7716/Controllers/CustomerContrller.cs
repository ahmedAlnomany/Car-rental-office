using Microsoft.AspNetCore.Mvc;
using ahmed7716.Models;

namespace ahmed7716.Controllers
{
    public class CustomerController : Controller
    {
        private static List<Customer> _customers = new List<Customer>();
        private static int _nextId = 1;

        public IActionResult Index()
        {
            return View(_customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CustomerID = _nextId++;
                _customers.Add(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Details(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customer customer)
        {
            if (id != customer.CustomerID) return NotFound();
            if (ModelState.IsValid)
            {
                var existing = _customers.FirstOrDefault(c => c.CustomerID == id);
                if (existing == null) return NotFound();

                existing.Name = customer.Name;
                existing.Phone = customer.Phone;
                existing.Email = customer.Email;
                existing.Adrss = customer.Adrss;
                existing.LicenseNumber = customer.LicenseNumber;

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null) return NotFound();

            _customers.Remove(customer);
            return RedirectToAction(nameof(Index));
        }
    }
}
