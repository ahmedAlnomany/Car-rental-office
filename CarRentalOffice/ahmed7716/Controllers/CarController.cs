using Microsoft.AspNetCore.Mvc;
using ahmed7716.Models;
using System.Linq;
using ahmed7716.Repositories;
using System.Diagnostics.Eventing.Reader;

namespace ahmed7716.Controllers
{
    public class CarController : Controller
    {
        private CarRepository Repository = new CarRepository();
        public IActionResult Index()
        {
            return View(Repository.GetAllCars());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                var addedCarId = Repository.AddCar(car);
                if (addedCarId!=0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception("something wrong");
                }
                
            }
            return View(car);
        }

        public IActionResult Details(int id)
        {
            var car = Repository.GetAllCars().FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        public IActionResult Edit(int id)
        {
            var car = Repository.GetAllCars().FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Car car)
        {
            if (id != car.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var carToUpdate = Repository.GetAllCars().FirstOrDefault(c => c.Id == id);
                if (carToUpdate == null) return NotFound();
                Repository.UpdateCar(car);
                return RedirectToAction(nameof(Index));
                
                
                               
            }
            return View(car);
        }

        public IActionResult Delete(int id)
        {
            var car = Repository.GetAllCars().FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car); 
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = Repository.GetAllCars().FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();

            Repository.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
