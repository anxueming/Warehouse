using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachSys.Controllers
{
    public class StudentsController : Controller
    {
        Models.TeachDBEntities tdb = new Models.TeachDBEntities();
        //
        // GET: /Students/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getStudents(int rows, int page)
        {
           
            int num = tdb.Students.Count();
            var stus = (from stu in tdb.Students
                       join cla in tdb.Classes on stu.ClassID equals cla.ID
                       orderby stu.ID
                       select new { 
                           ID = stu.ID,
                           Name = stu.Name,
                           ClassName = cla.Name,
                           Studentno=stu.StudentNo,
                           TelNo=stu.TelNo,
                           QQNo=stu.QQNo,
                           WeChatNo=stu.WeChatNo,
                           PTelNo1 = stu.PTelNo1,
                           PTelNo2 = stu.PTelNo2,
                           lsLogin=stu.IsLogin
                       }).Skip((page - 1) * rows).Take(rows);
            var obj = new { total = num, rows = stus };
            
            return Json(obj, JsonRequestBehavior.AllowGet);
           
        }
        public ActionResult getStudentsByMajor(int rows, int page,int majorID)
        {

            int num = tdb.Students.Count();
            var stus = (from stu in tdb.Students
                        join cla in tdb.Classes on stu.ClassID equals cla.ID
                        orderby stu.ID
                        where stu.ClassID==majorID
                        select new
                        {
                            ID = stu.ID,
                            Name = stu.Name,
                            ClassName = cla.Name,
                            Studentno = stu.StudentNo,
                            TelNo = stu.TelNo,
                            QQNo = stu.QQNo,
                            WeChatNo = stu.WeChatNo,
                            PTelNo1 = stu.PTelNo1,
                            PTelNo2 = stu.PTelNo2,
                            lsLogin = stu.IsLogin
                        }).Skip((page - 1) * rows).Take(rows);
            var obj = new { total = num, rows = stus };
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult getStudentsByClassesByMajor(int rows, int page, int classesID,int majorID)
        {

            int num = tdb.Students.Count();
            var stus = (from stu in tdb.Students
                        join cla in tdb.Classes on stu.ClassID equals cla.ID
                        orderby stu.ID
                        where stu.ClassID == classesID&&cla.majorID==majorID
                             
                        select new
                        {
                            ID = stu.ID,
                            Name = stu.Name,
                            ClassName = cla.Name,
                            Studentno = stu.StudentNo,
                            TelNo = stu.TelNo,
                            QQNo = stu.QQNo,
                            WeChatNo = stu.WeChatNo,
                            PTelNo1 = stu.PTelNo1,
                            PTelNo2 = stu.PTelNo2,
                            lsLogin = stu.IsLogin
                        }).Skip((page - 1) * rows).Take(rows);
            var obj = new { total = num, rows = stus };
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult add()
        {
            return View();
        }
        public ActionResult addStudents(Models.Students student)
        {
            try
            {
                student.Password = "123";
                
                tdb.Students.Add(student);
                tdb.SaveChanges();
                return Content("OK");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult edit(int id)
        {
            Models.Students student = tdb.Students.First(t => t.ID == id);
            return View(student);
        }
        public ActionResult editStudents(Models.Students s) {
            try
            {
                var ss = tdb.Students.First(t => t.ID == s.ID);
                ss.Name = s.Name;
                ss.ClassID = s.ClassID;
                ss.StudentNo = s.StudentNo;
                ss.TelNo = s.TelNo;
                ss.QQNo = s.QQNo;
                ss.WeChatNo = s.WeChatNo;
                ss.PTelNo1 = s.PTelNo1;
                ss.PTelNo2 = s.PTelNo2;
                ss.IsLogin = s.IsLogin;
                tdb.SaveChanges();
                return Content("OK");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult delectStudents(int ID)
        {
            try
            {
                var dep = tdb.Students.First(t => t.ID == ID);
                tdb.Students.Remove(dep);//给了一个删除的标记
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
                var stu = tdb.Students.First(t => t.ID == ID);
                stu.Password = "123";
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
