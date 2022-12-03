using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  //Fetching all the DataTables from Sql Server using Application DB Context
        }

        public IActionResult Index()
        {
            IEnumerable<Category> ObjCategoryList = _unitOfWork.Category.GetAll();   //Fetching all the rows of Category table and converting them to a list
            return View(ObjCategoryList);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name can't be the same as Display order!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj); //Adding Category object value into categories Table in DB
                _unitOfWork.Save();   //Saving changes into DB
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var CategoryFromDb = _db.Categories.Find(id);
            //var CategoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);
            var CategoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.id == id);

            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(CategoryFromDbFirst);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name can't be the same as Display order!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj); //Adding Category object value into categories Table in DB
                _unitOfWork.Save();   //Saving changes into DB
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var CategoryFromDb = _db.Categories.Find(id);
            //var CategoryFromDbSingle = _db.GetFirstOrDefault(u => u.id == id);
            var CategoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.id == id);

            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(CategoryFromDbFirst);
        }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Remove(obj); //Adding Category object value into categories Table in DB
                _unitOfWork.Save();   //Saving changes into DB
                TempData["success"] = "Category deleted successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
}
