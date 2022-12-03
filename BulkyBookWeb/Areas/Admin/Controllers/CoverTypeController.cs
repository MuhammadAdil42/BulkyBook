using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        public IUnitOfWork _UnitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;     //Incorporating unit of work in the application to access all DB related functions
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypes = _UnitOfWork.CoverType.GetAll(); //Getting all the values of CoverTypes from DB
            return View(coverTypes);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Cover Type created successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
           
                var GetFirstOrDefault = _UnitOfWork.CoverType.GetFirstOrDefault(u=>u.id == id);
                if(GetFirstOrDefault == null) 
                {
                    return NotFound();
                }

            return View(GetFirstOrDefault);
        }

        //Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj) 
        {
            if(ModelState.IsValid) 
            {
                _UnitOfWork.CoverType.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Cover Type updated successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var GetFirstOrDefault = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.id == id);
            if (GetFirstOrDefault == null)
            {
                return NotFound();
            }
            return View(GetFirstOrDefault);
        }

        //Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Remove(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Cover Type removed successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
