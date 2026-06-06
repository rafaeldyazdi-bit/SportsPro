using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class ValidationController : Controller
    {
        private SportsProContext context;
        public ValidationController(SportsProContext ctx) => context = ctx;

        public JsonResult CheckEmail(string email, int customerID)
        {
            if (customerID == 0)  // only check for new customers - don't check on edit
            {
                string msg = Check.EmailExists(context, email);
                if (!string.IsNullOrEmpty(msg))
                {
                    return Json(msg);
                }
            }

            TempData["okEmail"] = true;
            return Json(true);
        }
    }
}