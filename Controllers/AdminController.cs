using System.Linq;
using System.Web.Mvc;
using Library_Managment_Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Users()
    {
        var users = db.Users.ToList();
        return View(users);
    }

    [HttpPost]
    public ActionResult MakeLibrarian(string userId)
    {
        var userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(db));

        var user = userManager.FindById(userId);

        if (user == null)
            return Content("User not found");

        if (userManager.IsInRole(user.Id, "Librarian"))
            return Content("Already Librarian");

        userManager.AddToRole(user.Id, "Librarian");

        return Content("Role added successfully!");
    }
}