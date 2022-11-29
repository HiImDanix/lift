using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class QuestionsController1 : Controller
    {
        // GET: QuestionsController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: QuestionsController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuestionsController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionsController1/Create
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

        // GET: QuestionsController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestionsController1/Edit/5
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

        // GET: QuestionsController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestionsController1/Delete/5
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
