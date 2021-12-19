using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    
    public class ProductController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Policy="Permissions.Product.Edit")]
        public IActionResult Edit()
        {
            return View();
        }
        [Authorize(Policy = "Permissions.Product.Delete")]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
