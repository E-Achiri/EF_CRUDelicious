using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EF_CRUDelicious.Models;

namespace EF_CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;


        // public HomeController(ILogger<HomeController> logger)
        // {
        //     _logger = logger;
        // }

        // private MyContext dbContext;
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = _context.Dishes.ToList();
            ViewBag.AllDishes = _context.Dishes.OrderByDescending(u => u.CreatedAt);
            return View();
        }

        [HttpGet("newdirect")]
        public IActionResult DishDirect()
        {
            return RedirectToAction("DishCreator");
        }

        [HttpGet("new")]
        public IActionResult DishCreator()
        {
            return View();
        }

        [HttpPost("dishmaker")]
        public IActionResult DishResult(Dish nextdish)
        {
            _context.Add(nextdish);
            _context.SaveChanges();
            return RedirectToAction("DishDirect");
        }

        [HttpGet]
        [Route("dish/{id}")]
        public IActionResult DishDisplay(int id)
        {
            Console.WriteLine("I entered the /id route");
            ViewBag.Dish = _context.Dishes.FirstOrDefault(food => food.DishId == id);
            Console.WriteLine("I gotten past the /id route viewbag");
            return View("DishDisplay");
        }


        [HttpGet]
        [Route("edit/{dishid}")]
        public IActionResult DishEdit(int dishid)
        {
            ViewBag.Dish = _context.Dishes.FirstOrDefault(food => food.DishId == dishid);
            return View();
        }

        [HttpPost]
        [Route("DishEditor/{dishid}")]
        public IActionResult DishEditor(Dish fixeddish, int dishid)
        {
            Console.WriteLine("It got into the editor");

            //  = _context.Dishes.FirstOrDefault(food => food.DishId == dishid);
            Dish RetrievedDish = _context.Dishes.FirstOrDefault(user => user.DishId == dishid);
            // Then we may modify properties of this tracked model object
            RetrievedDish.Name = fixeddish.Name;
            RetrievedDish.Tastiness = fixeddish.Tastiness;
            RetrievedDish.ChefName = fixeddish.ChefName;
            RetrievedDish.Calories = fixeddish.Calories;
            RetrievedDish.Description = fixeddish.Description;

            RetrievedDish.UpdatedAt = DateTime.Now;

            // Finally, .SaveChanges() will update the DB with these new values
            _context.SaveChanges();
            // ViewBag.Dish = _context.Dishes.FirstOrDefault(user => user.DishId == dishid);
            Console.WriteLine("It past the editor");
            
            return Redirect($"/dish/{dishid}");
        }

        // Inside HomeController
        [HttpGet("delete/{Id}")]
        public IActionResult DeleteDish(int Id)
        {
            // Like Update, we will need to query for a single user from our Context object
            Dish RetrievedDish = _context.Dishes.SingleOrDefault(dish => dish.DishId == Id);

            // Then pass the object we queried for to .Remove() on Users
            _context.Dishes.Remove(RetrievedDish);

            // Finally, .SaveChanges() will remove the corresponding row representing this User from DB 
            _context.SaveChanges();
            // Other code
            return RedirectToAction("Index");
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
