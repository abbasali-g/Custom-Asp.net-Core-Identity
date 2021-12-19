using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            var allUsersExceptCurrentUser = await _userManager.Users.ToListAsync();
            //HttpContext.User.Claims.First(c => c.Type == "BranchCode").Value
            return View(allUsersExceptCurrentUser);
        }
    }
}
