using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {
        private const string TECH_KEY = "techID";

        private SportsProContext context { get; set; }
        public TechIncidentController(SportsProContext ctx) => context = ctx;

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Technicians = context.Technicians
                .Where(t => t.TechnicianID > -1)  // skip default unassigned value
                .OrderBy(c => c.Name)
                .ToList();

            var technician = new Technician();

            int? techID = HttpContext.Session.GetInt32(TECH_KEY);
            if (techID.HasValue)
            {
                technician = context.Technicians.Find(techID);
            }

            return View(technician);
        }

        [HttpPost]
        public IActionResult List(Technician technician)
        {
            if (technician.TechnicianID == 0)
            {
                TempData["message"] = "You must select a technician.";
                return RedirectToAction("Index");
            }
            else
            {
                HttpContext.Session.SetInt32(TECH_KEY, technician.TechnicianID);
                return RedirectToAction("List", new { id = technician.TechnicianID });
            }
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            var technician = context.Technicians.Find(id);
            if (technician == null)
            {
                TempData["message"] = "Technician not found. Please select a technician.";
                return RedirectToAction("Index");
            } 
            else
            {
                var model = new TechIncidentViewModel
                {
                    Technician = technician,
                    Incidents = context.Incidents
                        .Include(i => i.Customer)
                        .Include(i => i.Product)
                        .OrderBy(i => i.DateOpened)
                        .Where(i => i.TechnicianID == id)
                        .Where(i => i.DateClosed == null)
                        .ToList()
                };
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            int? techID = HttpContext.Session.GetInt32(TECH_KEY);
            if (!techID.HasValue)
            {
                TempData["message"] = "Technician not found. Please select a technician.";
                return RedirectToAction("Index");
            }
            
            var technician = context.Technicians.Find(techID);
            if (technician == null)
            {
                TempData["message"] = "Technician not found. Please select a technician.";
                return RedirectToAction("Index");
            } 
            else
            {
                var model = new TechIncidentViewModel
                {
                    Technician = technician,
                    Incident = context.Incidents
                        .Include(i => i.Customer)
                        .Include(i => i.Product)
                        .FirstOrDefault(i => i.IncidentID == id)!
                };
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
            Incident i = context.Incidents.Find(model.Incident.IncidentID)!;
            i.Description = model.Incident.Description;
            i.DateClosed = model.Incident.DateClosed;

            context.Incidents.Update(i);
            context.SaveChanges();

            int? techID = HttpContext.Session.GetInt32(TECH_KEY);
            return RedirectToAction("List", new { id = techID });
        }
    }
}