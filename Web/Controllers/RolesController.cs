using Microsoft.AspNetCore.Mvc;
using Web.Data.Entities;
using Web.ViewModels;

namespace Web.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Roles()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RolesAddPartial([FromBody] Roles usuario)
        {
            var rolesViewModel = new RolesViewModel();
        
            return PartialView("~/Views/Roles/Partial/rolesAddPartial.cshtml", rolesViewModel);
        }
    }
}
