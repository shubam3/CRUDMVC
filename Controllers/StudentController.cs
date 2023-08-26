using CRUDOperations_MVC.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDOperations_MVC.Controllers
{
    public class StudentController : Controller
    {
        DB_StudentEntities dbobj = new DB_StudentEntities();

            
        // GET: Student
        public ActionResult Student(tbl_student obj)
        {
            if(obj != null)
            return View(obj);

            else
                return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var EditedData = dbobj.tbl_student.Where(x => x.ID == id).First();
            return View("Student",EditedData);
        }

  


        [HttpPost]
        public ActionResult AddStudent(tbl_student model)
        {
            if (ModelState.IsValid)
            {
                tbl_student obj = new tbl_student();
                obj.ID = model.ID;
                obj.Name = model.Name;
                obj.Mobile = model.Mobile;
                obj.FName = model.FName;
                obj.Email = model.Email;
                obj.Description = model.Description;


                if (model.ID == 0)
                {
                    dbobj.tbl_student.Add(obj);
                    dbobj.SaveChanges();
                }
                else
                {
                    var EditedData = dbobj.tbl_student.Where(x => x.ID == model.ID).FirstOrDefault();
                    if (EditedData != null)
                    {
                        EditedData.Name = model.Name;
                        EditedData.FName = model.FName;
                        EditedData.Mobile = model.Mobile;
                        EditedData.Email = model.Email;
                        EditedData.Description = model.Description;
                        dbobj.SaveChanges();
                    }

                    
                   // dbobj.Entry(obj).State = EntityState.Modified;
                    //dbobj.SaveChanges();
                }
            }
            ModelState.Clear();
            return View("Student");
        }

        public ActionResult StudentList()
        {
            var StudentList = dbobj.tbl_student.ToList();
            return View(StudentList);
        }
        public ActionResult Delete(int id)
        {
            var DeletedData = dbobj.tbl_student.Where(x => x.ID == id).First();
            dbobj.tbl_student.Remove(DeletedData);
            dbobj.SaveChanges();

            var NewList = dbobj.tbl_student.ToList();
            return View("StudentList",NewList);
        }


    }
}