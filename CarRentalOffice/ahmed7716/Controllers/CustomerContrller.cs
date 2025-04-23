using ahmed7716.Models;
using ahmed7716.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ahmed7716.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerRepository repository = new CustomerRepository();
        public IActionResult Index()
        {
            return View(repository.GetAllCustomers());
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
                var addedId = repository.AddCustomer(customer);
                if (addedId != 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception("something wrong");
                }
            }
            return View(customer);
        }

        public IActionResult Details(int id)
        {
            var customer = repository.GetCustomerById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            var customer = repository.GetCustomerById(id);
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
                var existing = repository.GetCustomerById(id);
                if (existing == null) return NotFound();
                repository.UpdateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            var customer = repository.GetCustomerById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = repository.GetCustomerById(id);
            if (customer == null) return NotFound();
            repository.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
