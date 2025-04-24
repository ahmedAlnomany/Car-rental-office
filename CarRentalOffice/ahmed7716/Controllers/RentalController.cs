using ahmed7716.Models;
using ahmed7716.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ahmed7716.Controllers
{
    public class RentalController : Controller
    {
        private RentalRepository rentalRepository = new RentalRepository();
        public IActionResult Index()
        {
            return View(rentalRepository.GetAllRentals());
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

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rental rental)
        {

            rentalRepository.AddRental(rental);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {
            var rental = rentalRepository.GetRentalById(id);
            if (rental == null)
            {
                return NotFound();
            }

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


            rentalRepository.UpdateRental(rental);
            return RedirectToAction(nameof(Index));

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
            var Exitedrental = rentalRepository.GetRentalById(id);
            if (Exitedrental != null)
            {
                rentalRepository.DeleteRental(id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }

        }
    }
}
