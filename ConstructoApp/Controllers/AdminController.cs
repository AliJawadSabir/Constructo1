using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ConstructoApp.Models;
using System.ComponentModel.DataAnnotations;
namespace ConstructoApp.Controllers
{
    public class AdminController : Controller
    {
        Database1Entities4 db = new Database1Entities4();
        // GET: /Admin/
        DatabaseMaterialRate Dbmaterials = new DatabaseMaterialRate();
        public ActionResult Index()
        {
            List<MaterialRate> data = new List<MaterialRate>();
            var obj = Dbmaterials.MaterialRates.First(a => a.standard == "Low");
            var obj2 = Dbmaterials.MaterialRates.First(a => a.standard == "Medimum");
            var obj3 = Dbmaterials.MaterialRates.First(a => a.standard == "High");
            data.Add(obj);
            data.Add(obj2);
            data.Add(obj3);
            return View("Home",data);
        }
        public ActionResult imageUpload()
        {
            
            return View();
        }
         [HttpPost]
        public ActionResult imageUpload( HttpPostedFileBase  files)
        {
            
            SaveImage("file1");
            ViewBag.message = "Successfully Added";
            return View();
        }
         public ActionResult Login()
         {
             return View();
         }
         [HttpPost]
         public ActionResult AdminLogin(Admin ad)
         {
             Boolean flag = false;
             if (ad.user.CompareTo(ad.getUsername()) == 0 && ad.pass.CompareTo(ad.getpassword())==0)
             {
                 flag = true;
             }
             if (flag)
             {
                return Index();
             }
             ViewBag.error = "Email or password not match";
             return View("Login");
         }
         private void SaveImage(string file )
         {
             HttpPostedFileBase file1 = Request.Files[file];
             DateTime localDate = DateTime.Now;
             var fileName = Path.GetFileName(file1.FileName);
             string str = fileName;
             string[] str1 = str.Split('.');
             string time = localDate.ToString();
             string[] splitTime = time.Split(' ');
             string[] splitColon = splitTime[1].Split(':');
             string FIleName = str1[0] + splitColon[0] + "_" + splitColon[1] + "_" + splitColon[2];
             FIleName = FIleName + "." + str1[1];


             var path = Path.Combine(Server.MapPath("~/Content/uploads/"), FIleName);
             HouseImag img = new HouseImag();
             img.imagePath = path;
             db.HouseImags.Add(img);
             db.SaveChanges();
             file1.SaveAs(path);
         }
         public ActionResult UpdateMaterialRates()
         {
                          return View();
         }
        [HttpPost]
         public ActionResult UpdateMaterialRates(MaterialRate materials)
         {
             if (materials.standard == "Low")
             {
                 var obj = Dbmaterials.MaterialRates.First(a => a.standard == "Low");
                 var obj2 = obj;
                 if (materials.sand != 0)
                 {
                     obj2.sand = materials.sand;
                 }
                 if (materials.cement != 0)
                 {
                     obj2.cement = materials.cement;
                 }
                 if (materials.bricks != 0)
                 {
                     obj2.bricks = materials.bricks;
                 }
                 if (materials.steel != 0)
                 {
                     obj2.steel = materials.steel;
                 }
                 //Dbmaterials.MaterialRates.Remove(obj);
                 Dbmaterials.MaterialRates.Remove(obj);
                 Dbmaterials.SaveChanges();
                 //db.SaveChanges();
                 materials = obj2;
                 Dbmaterials.MaterialRates.Add(materials);
                 Dbmaterials.SaveChanges();

             }
             else if (materials.standard == "Medimum")
             {
                 var obj = Dbmaterials.MaterialRates.First(a => a.standard == "Medimum");
                 var obj2 = obj;
                 if (materials.sand != 0)
                 {
                     obj2.sand = materials.sand;
                 }
                 if (materials.cement != 0)
                 {
                     obj2.cement = materials.cement;
                 }
                 if (materials.bricks != 0)
                 {
                     obj2.bricks = materials.bricks;
                 }
                 if (materials.steel != 0)
                 {
                     obj2.steel = materials.steel;
                 }
                 //Dbmaterials.MaterialRates.Remove(obj);
                 Dbmaterials.MaterialRates.Remove(obj);
                 Dbmaterials.SaveChanges();
                 //db.SaveChanges();
                 materials = obj2;
                 Dbmaterials.MaterialRates.Add(materials);
                 Dbmaterials.SaveChanges();

             }
             else if (materials.standard == "High")
             {
                 var obj = Dbmaterials.MaterialRates.First(a => a.standard == "High");
                 var obj2 = obj;
                 if (materials.sand != 0)
                 {
                     obj2.sand = materials.sand;
                 }
                 if (materials.cement != 0)
                 {
                     obj2.cement = materials.cement;
                 }
                 if (materials.bricks != 0)
                 {
                     obj2.bricks = materials.bricks;
                 }
                 if (materials.steel != 0)
                 {
                     obj2.steel = materials.steel;
                 }
                 //Dbmaterials.MaterialRates.Remove(obj);
                 Dbmaterials.MaterialRates.Remove(obj);
                 Dbmaterials.SaveChanges();
                 //db.SaveChanges();
                 materials = obj2;
                 Dbmaterials.MaterialRates.Add(materials);
                 Dbmaterials.SaveChanges();

             }
             else
             {
                 Dbmaterials.MaterialRates.Add(materials);
                 Dbmaterials.SaveChanges();
             }
             ViewBag.message = "Successfully Updated";
             return View();
         }
    }
}
