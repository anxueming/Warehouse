using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachSys.Controllers
{
    public class DepartmentsController : Controller
    {
        Models.TeachDBEntities tdb = new Models.TeachDBEntities();
        //
        // GET: /Departments/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult addIndex()
        {
            return View();
        }
        public ActionResult EditIndex(int id)
        {
            Models.Departments department = tdb.Departments.First(t => t.ID == id);
            return View(department);
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDepartments()
        {
            var deps = from dep in tdb.Departments
                       select new { ID = dep.ID, Name = dep.Name };
            return Json(deps, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据部门ID删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteDepartments(int id)
        {
            try 
            {
            var dep=tdb.Departments.First(t=>t.ID==id);
            tdb.Departments.Remove(dep);//给了一个删除的标记
            tdb.SaveChanges();
            return Content("OK");
            }
            catch
            {
            return Content("Err");
            }
        }

      //通过ID查询
        public ActionResult GetDepartmentsByID(int id)
        {
            try
            {
                var depa = from dep in tdb.Departments
                           where dep.ID == id
                           select new { ID = dep.ID, Name = dep.Name };
                return Json(depa,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Content("err");
            }
          
        }
        
          //修改Departments表
        public ActionResult EditDepartments(int id, string name)
        {
            try
            {
                var dep = tdb.Departments.First(t => t.ID == id);
                dep.Name = name;
                tdb.SaveChanges();
                return Content("OK");
            }
            catch
            {
                return Content("err");
            }
        }

        //向Departments表内添加内容
        public ActionResult addDepartments(string name)
        {
            
            
                try
                {
                    var dep = new Models.Departments();
                    dep.Name = name;
                    tdb.Departments.Add(dep);
                    tdb.SaveChanges();
                    return Content("OK");
                }
                catch
                {
                    return Content("Err");
                }
            
        }
    }
}
