using BusinessLayer;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLOG_PROJESİ.Controllers
{
    public class AdminCategoryController : Controller
    {
        readonly CategoryManager cm = new CategoryManager(new EfCategoryDal());

        [Authorize(Roles="A")]
        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                var categoryvalues = cm.GetList();
                return View(categoryvalues);
            }

            return View();
        }
        [HttpGet]
        public ActionResult AddCategory()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category p)
        {
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult result = categoryValidator.Validate(p);
            if (result.IsValid)
            {
                cm.CategoryAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            
            return View();
        }

        public ActionResult DeleteCategory(int id)
        {
            var category = cm.GetByID(id);
            cm.CategoryDelete(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var category = cm.GetByID(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            cm.CatetegoryUpdate(category);
            return RedirectToAction("Index");
            
        }




    }
}