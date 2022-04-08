using BusinessLayer;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FluentValidation.Results;
using BusinessLayer.ValidationRules;

namespace BLOG_PROJESİ.Controllers
{
    public class WriterPanelController : Controller
    {
        // GET: WriterPanel
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        Context context = new Context();

        [HttpGet]
        public ActionResult WriterProfile(int id=0)
        {
            string p = (string)Session["WriterMail"];

            id = context.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var writervalue = wm.GetByID(id);            
            return View(writervalue);
        }

        [HttpPost]
        public ActionResult v(Writer writer)
        {
            WriterValidator writervalidator = new WriterValidator();
            ValidationResult result = writervalidator.Validate(writer);
            if (result.IsValid)
            {
                wm.WriterUpdate(writer);
                return RedirectToAction("AllHeading","WriterPanel");
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

        public ActionResult MyHeading(string p)
        {
            
            p = (string)Session["WriterMail"];
            var writeridinfo = context.Writers.Where(x => x.WriterMail == p).Select(y=>y.WriterID).FirstOrDefault();
            var values = hm.GetListByWriter(writeridinfo);
            return View(values);
        } 
        [HttpGet]
        public ActionResult NewHeading()
        {
           
            List<SelectListItem> valuecategory = (from x in cm.GetList() select new SelectListItem { Text = x.CategoryName, Value = x.CategoryID.ToString() }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            string t = (string)Session["WriterMail"];
            var writeridinfo = context.Writers.Where(x => x.WriterMail == t).Select(y => y.WriterID).FirstOrDefault();
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.WriterID = writeridinfo;
            heading.HeadingStatus = true;
            hm.HeadingAdd(heading);
            return RedirectToAction("MyHeading");
        }
        [HttpGet]
        public ActionResult EditHeading(int id)
        {

            List<SelectListItem> valuecategory = (from x in cm.GetList() select new SelectListItem { Text = x.CategoryName, Value = x.CategoryID.ToString() }).ToList();
            ViewBag.vlc = valuecategory;


            var HeadingValue = hm.GetByID(id);
            return View(HeadingValue);
        }
        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteHeading(int id)
        {
            var HeadingValue = hm.GetByID(id);
            HeadingValue.HeadingStatus = false;
            hm.HeadingDelete(HeadingValue);
            return RedirectToAction("MyHeading");
        }
        public ActionResult ActiveHeading(int id)
        {
            var HeadingValue = hm.GetByID(id);
            HeadingValue.HeadingStatus = true;
            hm.HeadingActive(HeadingValue);
            return RedirectToAction("MyHeading");
        }
        public ActionResult AllHeading(int p = 1)
        {
            var resultheading = hm.GetList().ToPagedList(p, 4);
            return View(resultheading);
        }
    }
}