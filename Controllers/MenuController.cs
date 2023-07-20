using HumHum.Areas.Identity.Data;
using HumHum.Models;
using HumHum.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumHum.Controllers
{
    public class MenuController : Controller
    {
        private readonly IRepositoryBase<FoodItem, long> _repository;
        private readonly IRepositoryBase<Restaurant, long> _Restaurentrepository;
        public MenuController(IRepositoryBase<FoodItem, long> repository,
            IRepositoryBase<Restaurant, long> Restaurentrepository
            ) {
            _repository = repository; 
            _Restaurentrepository = Restaurentrepository;   
        } 
        public IActionResult Index(List<FoodItem> foodItems) {
            //check if the list has items and if not fill it with all items
            if(foodItems.Count() > 0)
            {
                return View(foodItems);
            }
            foodItems = _repository.Find(item => item != null, false, item => item.Restaurant).ToList();
            // loop and make sure that every item has at least a default img
            //foreach (FoodItem item in foodItems)
            //{
            //    if (item.Image == null || item.Image == "") { item.Image = defaultfooditems.Defaultfooditem; _repository.Update(item); }
            //}

            return View(foodItems);
        }


        [HttpGet("Menu/GetSearchResult/{search}/{category}")]
        public ActionResult GetSearchResult (string category,string search) {
            if (category.Equals("Food")) {
                List<FoodItem> fooditems = _repository.Find(item => item.FoodItemName.Contains(search),false,item => item.Restaurant).ToList();
                if(fooditems.Count() <= 0) return RedirectToAction("NotFound", "Home", new { msg = search });
                TempData["SearchValue"] = search;
                return View("Index",fooditems);
            }
            if (category.Equals("Resturant"))
            {
                List<Restaurant> restaurants = _Restaurentrepository.Find(item => item.RestaurantName.Contains(search)).ToList();
                if (restaurants.Count() <= 0) return RedirectToAction("NotFound", "Home",new { msg = search });
                List<FoodItem> fooditems = new();
                foreach(var item in restaurants)
                {
                    fooditems.AddRange(_repository.Find(fooditem => fooditem.RestaurantId == item.RestaurantId,false,fooditem => fooditem.Restaurant));
                }
                TempData["SearchValue"] = search;
                return View("Index", fooditems);
            }
            return RedirectToAction("NotFound","Home");
        }
        //public IActionResult Details(int id)
        //{
        //    var item = items.Find(x => x.FoodItemId == id);
        //    return View(item);
        //}
    }
}
