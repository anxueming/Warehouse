using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachSys.Controllers
{
    public class ClassesController : Controller
    {
        Models.TeachDBEntities tdb = new Models.TeachDBEntities();
        //
        // GET: /Classes/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult add()
        {
            return View();
        }
        public ActionResult edit() 
        {
            return View();
        }
        public ActionResult getClasseslistByMajorId(int majorID)
        {
            var classes = from c in tdb.View_TeacherClasses
                          select new
                          {
                              ID = c.ID,
                              Name = c.Name,
                              majorID = c.majorID,
                              TeacherName = c.TeacherName,
                              TeacherNo = c.TeacherNo
                          };
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
           
        public ActionResult getClasseslistByMajorIdWithDirector(int majorID)
        {
            var classes = from c in tdb.View_TeacherClasses
                          select new
                          {
                              ID = c.ID,
                              Name = c.Name,
                              majorID=c.majorID,
                              TeacherName = c.TeacherName,
                              TeacherNo = c.TeacherNo
                          };
            if (majorID != -1) {
                classes = classes.Where(t => t.majorID == majorID);

            }
         return Json(classes,JsonRequestBehavior.AllowGet);
        }
        public ActionResult addClasses(Models.Classes classes)
        {
            try
            {
                tdb.Classes.Add(classes);
                tdb.SaveChanges();
                return Content("OK");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult getClasses()
        {
            var majs = from c in tdb.Classes
                       join m in tdb.Majors on c.majorID equals m.ID
                       select new
                       {
                           ID = c.ID,
                           majorName = m.Name,
                           Name = c.Name
                       };
            return Json(majs);
        }
        public ActionResult deleteClasses(int ID)
        {
            try
            {
                var classes = tdb.Classes.First(t => t.ID == ID);
                tdb.Classes.Remove(classes);
                tdb.SaveChanges();
                return Content("OK");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult getClassesById(int ID)//通过ID查找班级名称
        {
            try
            {
                var classes = from cla in tdb.Classes
                             where cla.ID == ID
                             select new
                             {
                                 ID = cla.ID,
                                 majID = cla.majorID,
                                 Name = cla.Name
                             };
                return Json(classes, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult editClasses(int ID, int majID, string Name)//修改班级
        {
            try
            {
                var classes = tdb.Classes.First(t => t.ID == ID);
                classes.majorID = majID;
                classes.Name = Name;
                tdb.SaveChanges();
                return Content("OK");
            }
            catch
            {
                return Content("err");
            }
        }
    }
}
