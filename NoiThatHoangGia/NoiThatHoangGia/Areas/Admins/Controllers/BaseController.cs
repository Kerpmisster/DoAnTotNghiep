using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NoiThatHoangGia.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class BaseController : Controller, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { area = "Admins", controller = "Login", action = "Index" }));
            }
            base.OnActionExecuting(context);
        }
    }
}
