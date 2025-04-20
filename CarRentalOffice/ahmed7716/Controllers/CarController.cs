using Microsoft.AspNetCore.Mvc;
using ahmed7716.Models; 

namespace ahmed7716.Controllers
{
    public class CarController : Controller
    {
        private static List<Car> _cars = new List<Car>(); 
        private static int _nextId = 1; 

        
        public IActionResult Index()
        {
            return View(_cars);
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
                car.Id = _nextId++;
                _cars.Add(car); 
                return RedirectToAction(nameof(Index)); 
            }
            return View(car); 
        }

        
        public IActionResult Details(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        
        public IActionResult Edit(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
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
                var carToUpdate = _cars.FirstOrDefault(c => c.Id == id);
                if (carToUpdate == null) return NotFound();

                carToUpdate.Name = car.Name;
                carToUpdate.Model = car.Model;
                carToUpdate.Price = car.Price;

                return RedirectToAction(nameof(Index)); 
            }
            return View(car); 
        }

        
        public IActionResult Delete(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();

            _cars.Remove(car); 
            return RedirectToAction(nameof(Index)); 
        }
    }
}