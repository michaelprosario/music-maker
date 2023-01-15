using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MusicMakerMvc.Controllers
{
    public class ArpeggioController : Controller
    {
        // GET: ArpeggioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ArpeggioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArpeggioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArpeggioController/Create
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

        // GET: ArpeggioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArpeggioController/Edit/5
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

        // GET: ArpeggioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArpeggioController/Delete/5
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
