using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachSys.Controllers
{
    public class MajorsController : Controller
    {
        Models.TeachDBEntities tdb = new Models.TeachDBEntities();
        //
        // GET: /Majors/
      
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult add()
        {

            return View();
        }
        public ActionResult edit(int id)
        {
            Models.Majors majors = tdb.Majors.First(t => t.ID == id);
            return View(majors);
        }
        public ActionResult getMajors() 
        {
            var depid = App_Start.Helper.GetDepartmentID(HttpContext);
            var majs = from maj in tdb.Majors 
                       join dep in tdb.Departments on maj.DepartmentID equals dep.ID
                       where maj.DepartmentID == depid
                       select new { 
                           ID = maj.ID,
                           Name=maj.Name,
                           Status = maj.Statue };
            return Json(majs);
        }
        public ActionResult getMajorsByDepartmentID(int deptID)
        {
            var majs = from m in tdb.Majors
                       where m.DepartmentID==deptID
                       select new
                       {
                           ID = m.ID,
                           Name = m.Name,
                         
                       };
            return Json(majs);
        }
        public ActionResult addMajors(Models.Majors major)//添加专业
        {
            var depid = App_Start.Helper.GetDepartmentID(HttpContext);
            try
            {
                //var maj = new Models.Majors();
                //maj.Name = major.Name;
                //maj.DepartmentID = depid;
                major.DepartmentID = depid;
                tdb.Majors.Add(major);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }

        public ActionResult getMajorsById(int ID)//通过ID查找专业名称
        {
            try
            {
                var majors = from maj in tdb.Majors
                             where maj.ID == ID
                             select new
                             {  
                                 ID=maj.ID,
                                 depID = maj.DepartmentID,
                                 Name = maj.Name
                             };
                return Json(majors, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Content("err");
            }
        }
        public ActionResult editMajors(int ID,string Name)//修改专业
        {
            try
            {
                var major = tdb.Majors.First(t => t.ID == ID);
              
                major.Name = Name;
                tdb.SaveChanges();
                return Content("OK");
            }
            catch {
                return Content("err");
            }
        }
        public ActionResult deleteMajors(int ID)//根据ID删除数据
        {
            try
            {
                var majors = tdb.Majors.First(t => t.ID == ID);
                tdb.Majors.Remove(majors);
                tdb.SaveChanges();
                return Content("OK");
            }
            catch {
                return Content("err");
            }
            
         }
    }
}
