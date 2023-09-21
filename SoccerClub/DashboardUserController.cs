using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SoccerClub
{
    public class DashboardUserController : Controller
    {
        // GET: DashboardUserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DashboardUserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashboardUserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardUserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardUserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashboardUserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardUserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashboardUserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
