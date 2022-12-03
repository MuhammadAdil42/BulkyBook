using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Web.WebPages.Html;


namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  //Fetching all the DataTables from Sql Server using Application DB Context
        }

        public IActionResult Index()
        {
            IEnumerable<Category> ObjCategoryList = _unitOfWork.Category.GetAll();   //Fetching all the rows of Category table and converting them to a list
            return View(ObjCategoryList);
        }

        //Get
        public IActionResult Upsert(int? id)
        {
            ProductVM productVm = new()
            {
                product = new(),    

                CategoryList = (IEnumerable<System.Web.Mvc.SelectListItem>)_unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.id.ToString()
                }),
                CoverTypeList = (IEnumerable<System.Web.Mvc.SelectListItem>)_unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.id.ToString()
                }),
            };


            if (id == null || id == 0)
            {
                //Create Product
                //ViewBag.CatList = CategoryList;
                // ViewData["CovTypeList"] = CoverTypeList;
                Console.WriteLine("I'm stopping here!");
                return View(productVm);
            }

            else
            {
                //Update Product
            }

            return View(productVm);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
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
