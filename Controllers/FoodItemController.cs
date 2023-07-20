using HumHum.Repository;
using HumHum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HumHum.ViewModels;
using Microsoft.AspNetCore.Identity;
using HumHum.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace HumHum.Controllers
{
    [Authorize]
    public class FoodItemController : Controller
    {
        private readonly IRepositoryBase<FoodItem, long> _foodItemRepository;
        private readonly IRepositoryBase<Restaurant, long> _restaurantRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper _mapper;
        public FoodItemController(IRepositoryBase<FoodItem, long> foodItemRepository,
            IRepositoryBase<Restaurant, long> restaurantRepository,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment WebHostEnvironment,
            IMapper mapper
            )
        {
            _foodItemRepository = foodItemRepository;
            _restaurantRepository = restaurantRepository;
            _signInManager = signInManager;
            webHostEnvironment = WebHostEnvironment;
            _mapper = mapper;
        }
        // GET: FoodItemController
        public ActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_foodItemRepository.Find(x => x.RestaurantId > 0, false, x => x.Restaurant));
        }

        // GET: FoodItemController/Create
        public ActionResult Create()
        {
            FoodItemCreateViewModel foodItemCreateViewModel = new FoodItemCreateViewModel()
            {
                restaurants = _restaurantRepository.List().ToList()
            };
            return View(foodItemCreateViewModel);
        }

        // POST: FoodItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodItemCreateViewModel foodItemCreateViewModel)
        {
            string uniqueFileName;
            if (!foodItemCreateViewModel.Image.FileName.Equals("Default Image"))
            {
                uniqueFileName = UploadedFile(foodItemCreateViewModel);
            }
            else
            {
                uniqueFileName = foodItemCreateViewModel.DefaultImage;
            }
            try
            {
                FoodItem foodItem = new() { 
                    FoodItemName = foodItemCreateViewModel.FoodItemName,
                    Image=uniqueFileName,
                    Price=foodItemCreateViewModel.Price,
                    RestaurantId=foodItemCreateViewModel.RestaurantId
                };
                _foodItemRepository.Create(foodItem);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private string UploadedFile(FoodItemCreateViewModel model)
        {
            string? uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            //uniqueFileName = "/images/" + uniqueFileName;
            return uniqueFileName??= "Default_img.jpg";
        }
        private string EditedFile(EditFoodItem model)
        {
            string? uniqueFileName = null;

            if (model.Editedimg != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Editedimg.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Editedimg.CopyTo(fileStream);
                }
            }
            //uniqueFileName = "/images/" + uniqueFileName;
            return uniqueFileName ??= "Default_img.jpg";
        }
        // GET: FoodItemController/Edit/5
        public ActionResult Edit(int id)
        {
            EditFoodItem editFood = _mapper.Map<EditFoodItem>(_foodItemRepository.GetById(id));
            return View(editFood);
        }

        // POST: FoodItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditFoodItem editFoodItem)
        {
            try
            {
                FoodItem food = _mapper.Map<FoodItem>(editFoodItem);
                string imgurl = food.Image;

                //Check if the image has been changed (Chose a new one)
                if (!editFoodItem.Editedimg.FileName.Equals("Default Image"))
                {
                    //delate the image if not the default 
                    if (!imgurl.Equals("Default_img.jpg"))
                    {
                        imgurl = Path.Combine(webHostEnvironment.WebRootPath, "images", imgurl);
                        FileInfo fileInfo = new FileInfo(imgurl);
                        if (!fileInfo.Exists)
                        {
                            System.IO.File.Delete(imgurl);
                            fileInfo.Delete();
                        }
                    }//if
                    //add the new Photo in the system
                    string Filename = EditedFile(editFoodItem);
                    // check if the image has been added to the System
                    if (Filename!= null)
                    {
                        food.Image = Filename;
                    }
                }//if
                    _foodItemRepository.Update(food);
                return RedirectToAction("Index");
            }//try 
            catch
            {
                return View();
            }
        }

        // GET: FoodItemController/Delete/5
        public ActionResult Delete(int id)
        {
            var foodItem = _foodItemRepository.GetById(id);
            return View(foodItem);
        }

        // POST: FoodItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FoodItem fooditem)
        {
            try
            {
                var food = _foodItemRepository.GetById((long)id);
                string imgurl = food.Image;
                if (!imgurl.Equals("Default_img.jpg"))
                {
                    imgurl = Path.Combine(webHostEnvironment.WebRootPath, "images", imgurl);
                    FileInfo fileInfo = new FileInfo(imgurl);
                    if (fileInfo.Exists)
                    {
                        System.IO.File.Delete(imgurl);
                        fileInfo.Delete();
                    }
                }
                _foodItemRepository.Delete(food);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
