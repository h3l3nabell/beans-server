using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace beans_server.Controllers
{
    public class BeansController : Controller
    {
        // GET: BeansController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BeansController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BeansController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BeansController/Create
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

        // GET: BeansController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BeansController/Edit/5
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

        // GET: BeansController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BeansController/Delete/5
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
