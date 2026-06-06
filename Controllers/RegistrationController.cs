using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        private SportsProContext context { get; set; }
        public RegistrationController(SportsProContext ctx) => context = ctx;

        [HttpGet]
        public IActionResult Index()
        {
            var model = new RegistrationViewModel { 
                Customer = new Customer(),
                Customers = context.Customers
                    .OrderBy(c => c.LastName)
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(RegistrationViewModel model)
        {
            if (model.HasCustomer)
            {
                return RedirectToAction("List", new { id = model.Customer.CustomerID });
            }
            else
            {
                TempData["message"] = "You must select a customer.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Route("[controller]s/{id?}")]
        public IActionResult List(int id)
        {
            // get selected customer and related products 
            var model = new RegistrationViewModel
            {
                Customer = context.Customers
                    .Include("Products")
                    .Where(c => c.CustomerID == id)
                    .FirstOrDefault()!
            };

            if (model.HasCustomer)
            {
                // get list of products for drop-down and display view
                model.Products = context.Products
                    .OrderBy(p => p.Name)
                    .ToList();
                return View(model);
            } 
            else
            {
                TempData["message"] = "Customer not found. Please select a customer.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if (model.HasProduct)
            {
                // get customer and product from database
                model.Customer = context.Customers
                    .Include("Products")
                    .Where(c => c.CustomerID == model.Customer.CustomerID)
                    .FirstOrDefault()!;
                model.Product = context.Products.Find(model.Product.ProductID)!;

                if (model.HasCustomer && model.HasProduct)
                {
                    if (model.Customer.Products.Contains(model.Product))
                    {
                        TempData["message"] = $"{model.Product.Name} is already registered to {model.Customer.FullName}";

                        // re-display view
                        model.Products = context.Products
                            .OrderBy(p => p.Name)
                            .ToList();
                        return View("List", model);
                    }
                    else  
                    {   
                        model.Customer.Products.Add(model.Product);
                        context.SaveChanges();
                        TempData["message"] = $"{model.Product.Name} has been registered to {model.Customer.FullName}";
                    }
                }
            }
            else  // no product selected
            {
                TempData["message"] = "You must select a product.";
            }

            return RedirectToAction("List", new { ID = model.Customer.CustomerID });
        }

        [HttpPost]
        public IActionResult Delete(RegistrationViewModel model)
        {
            // get customer and product from database
            model.Customer = context.Customers
                .Include("Products")
                .Where(c => c.CustomerID == model.Customer.CustomerID)
                .FirstOrDefault()!;
            model.Product = context.Products.Find(model.Product.ProductID)!;

            if (model.HasCustomer && model.HasProduct)
            {
                model.Customer.Products.Remove(model.Product);
                context.SaveChanges();
                TempData["message"] = $"{model.Product.Name} has been de-registered from {model.Customer.FullName}";
            }

            return RedirectToAction("List", new { ID = model.Customer.CustomerID });
        }
    }
}