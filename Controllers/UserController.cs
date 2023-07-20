using AutoMapper;
using HumHum.Areas.Identity.Data;
using HumHum.Repository;
using HumHum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumHum.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IRepositoryBase<ApplicationUser, string> _userRepository;
        private readonly IMapper _mapper;
        public UserController(IRepositoryBase<ApplicationUser, string> userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        // GET: UserController
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            List<ApplicationUser> users = _userRepository.List().ToList();
            List<UserTable> usersTable = _mapper.Map<List<UserTable>>(users);
            return View(usersTable);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
