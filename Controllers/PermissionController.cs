using Identity.Data;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    //[Authorize(Roles = "SuperAdmin")]
    [Authorize]
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public PermissionController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<ActionResult> Index(string roleId= "e030cb93-e180-4695-9f57-caa5d7bddd72")
        {
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(Permissions.Product), roleId);
            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = allPermissions;
            return View(model);
        }
        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }
            return RedirectToAction("Index", new { roleId = model.RoleId });
        }
    }
}
