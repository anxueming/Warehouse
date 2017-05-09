using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TeachSys.Controllers
{
    public class TeachersController : Controller
    {
        //
        // GET: /Teachers/
        Models.TeachDBEntities tdb = new Models.TeachDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getTeachers(int rows,int page)
        {
            var depid = App_Start.Helper.GetDepartmentID(HttpContext);
            int num = tdb.Teachers.Count();
            var majs = (from t in tdb.Teachers
                        join d in tdb.Departments on t.DeptID equals d.ID
                        orderby t.ID
                        where d.ID == depid
                        select new
                        {
                            ID = t.ID,
                            TeacherNo = t.TeacherNo,
                            DeptName = d.Name,
                            Name = t.Name,
                            IsLogin = t.IsLogin
                        }).Skip((page - 1) * rows).Take(rows);
            var obj=new{ total=num,rows=majs};
            return Json(obj,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTeachersList()
        {
            FormsAuthenticationTicket ti = TeachSys.App_Start.Helper.GetTicket(HttpContext);
            int deptid = App_Start.Helper.GetDepartmentID(HttpContext);
            var teachers = from t in tdb.Teachers
                           where t.DeptID == deptid
                           select new
                           {
                               ID = t.ID,
                               Name = t.Name
                           };
            ;

            return Json(teachers, JsonRequestBehavior.AllowGet);
        }
        public ActionResult add()
        {
            return View();
        }
        public ActionResult AddTeachers(Models.Teachers tec)
        {
            var depid = App_Start.Helper.GetDepartmentID(HttpContext);
            try
            {
                tec.Password = "123";
                tec.DeptID = depid;
                tdb.Teachers.Add(tec);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult deleteTeacher(int ID)
        {
            try
            {
                var dep = tdb.Teachers.First(t => t.ID == ID);
                tdb.Teachers.Remove(dep);//给了一个删除的标记
                tdb.SaveChanges();
                return Content("OK");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult resultpass(int ID)
        {
            try
            {
                var dep = tdb.Teachers.First(t => t.ID == ID);
                dep.Password = "123";
                tdb.SaveChanges();
                return Content("OK");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult getTeacherById(int ID)
        {
            try
            {
                var ta = from t1 in tdb.Teachers
                         where t1.ID == ID
                           select new { 
                               ID = t1.ID,
                               Name = t1.Name, 
                               TeacherNo = t1.TeacherNo,
                               IsLogin=t1.IsLogin
                           };
                return Json(ta, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Content("err");
            }
           
        }
        public ActionResult edit(int id)
        {
            Models.Teachers teacher = tdb.Teachers.First(t => t.ID == id);
            return View(teacher);
        }
        public ActionResult editTeacher(Models.Teachers tt)
        {
            try
            {
                
                var ta = tdb.Teachers.First(t => t.ID == tt.ID);
                ta.Name = tt.Name;
                ta.TeacherNo = tt.TeacherNo;
                ta.IsLogin = tt.IsLogin;
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
