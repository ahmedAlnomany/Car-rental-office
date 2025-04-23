using ahmed7716.Models;
using ahmed7716.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ahmed7716.Controllers
{
    public class RentalController : Controller
    {
        private RentalRepository rentalRepository = new RentalRepository();
        private CustomerRepository customerRepository = new CustomerRepository();
        private CarRepository carRepository = new CarRepository();

        public IActionResult Index()
        {
            var rentals = rentalRepository.GetAllRentals();
            return View(rentals);
        }

        public IActionResult Details(int id)
        {
            var rental = rentalRepository.GetRentalById(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = customerRepository.GetAllCustomers();
            ViewBag.Cars = carRepository.GetAllCars();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                rentalRepository.AddRental(rental);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = customerRepository.GetAllCustomers();
            ViewBag.Cars = carRepository.GetAllCars();
            return View(rental);
        }

        public IActionResult Edit(int id)
        {
            var rental = rentalRepository.GetRentalById(id);
            if (rental == null)
            {
                return NotFound();
            }

            ViewBag.Customers = customerRepository.GetAllCustomers();
            ViewBag.Cars = carRepository.GetAllCars();
            return View(rental);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Rental rental)
        {
            if (id != rental.RentalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                rentalRepository.UpdateRental(rental);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = customerRepository.GetAllCustomers();
            ViewBag.Cars = carRepository.GetAllCars();
            return View(rental);
        }

        public IActionResult Delete(int id)
        {
            var rental = rentalRepository.GetRentalById(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            rentalRepository.DeleteRental(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
