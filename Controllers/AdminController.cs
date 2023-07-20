using HumHum.Repository;
using HumHum.Models;
using HumHum.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HumHum.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HumHum.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IRepositoryBase<Order, long> _orderRepository;
        private readonly IRepositoryBase<FoodItem, long> _foodItemRepository;
        private readonly IRepositoryBase<Restaurant, long> _restaurantRepository;
        private readonly IRepositoryBase<ApplicationUser, string> _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AdminController(
            IRepositoryBase<Order, long> orderRepository,
            IRepositoryBase<FoodItem,long> foodItemRepository,
            IRepositoryBase<Restaurant, long> restaurantRepository,
            IRepositoryBase<ApplicationUser, string> userRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _orderRepository = orderRepository;
            _foodItemRepository = foodItemRepository;
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: AdminController
        public ActionResult Index() => RedirectToAction("Dashboard");
        public ActionResult Dashboard()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            IEnumerable<Order> _orders = _orderRepository.List();
            IEnumerable<FoodItem> _foodItems = _foodItemRepository.List();
            IEnumerable<Restaurant> _restaurants = _restaurantRepository.List();
            IEnumerable<ApplicationUser> _users = _userRepository.List();
            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                orders = _orders,
                restaurants = _restaurants,
                foodItems = _foodItems,
                applicationUsers = _users
            };
            return View(dashboardViewModel);
        }

    }
}
