using Microsoft.AspNetCore.Mvc;
using ahmed7716.Models;
using System.Collections.Generic;
using System.Linq;

namespace ahmed7716.Controllers
{
    public class EmployeeController : Controller
    {
        private static List<Employee> _employees = new List<Employee>();
        private static int _nextId = 1;

        public IActionResult Index()
        {
            return View(_employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.EmployeeID = _nextId++;
                _employees.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Details(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.EmployeeID == id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.EmployeeID == id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeID) return NotFound();

            if (ModelState.IsValid)
            {
                var existing = _employees.FirstOrDefault(e => e.EmployeeID == id);
                if (existing == null) return NotFound();

                existing.Name = employee.Name;
                existing.Position = employee.Position;
                existing.Phone = employee.Phone;
                existing.Email = employee.Email;
                existing.Address = employee.Address;

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.EmployeeID == id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.EmployeeID == id);
            if (employee == null) return NotFound();

            _employees.Remove(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}
