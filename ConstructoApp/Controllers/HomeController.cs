using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConstructoApp.Models;
using System.Data.Entity;
using System.IO;
using System.ComponentModel.DataAnnotations;
namespace ConstructoApp.Controllers
{
    public class HomeController : Controller
    {
        Database1Entities  db = new Database1Entities();
        Database1Entities3 mapdb = new Database1Entities3();
        Database1Entities5 userDb = new Database1Entities5();
        Database1Entities4 housbd = new Database1Entities4();
        DatabaseMaterialRate Dbmaterials = new DatabaseMaterialRate();
        String f_result = "";
        string text = "";
        string num = "";
        string text1 = "";
        string num2 = "";

        // GET: /Home/
        public double LentarCement(double RoofLength, double RoofWidth, double RoofThickness)
        {
            double area = RoofLength * RoofWidth * RoofThickness;
            double temp1 = area * 1.54;
            double cement = temp1 / 7;
            return cement;


        }
        public double LentarCrush(double RoofLength, double RoofWidth, double RoofThickness)
        {
            double crush = LentarCement(RoofLength, RoofWidth, RoofThickness);
            crush = crush * 4;
            return crush;
        }
        public double LentarSand(double RoofLength, double RoofWidth, double RoofThickness)
        {
            double sand = LentarCement(RoofLength, RoofWidth, RoofThickness);
            sand = sand * 2;
            return sand;
        }
        public double LanterSteel(double RoofLength, double RoofWidth, double RoofThickness)
        {
            double steel = 3.06;
            double area = RoofLength * RoofWidth * RoofThickness;
            steel = steel * area;
            return steel;

        }
        public ActionResult Index()
        {
            return View("Login");
        }
        public ActionResult Gallery()
        {

            List<HouseImages> data = new List<HouseImages>();
            
            string var = null;
            var query = from b in housbd.HouseImags
                        orderby b.imagePath
                        select b;
            int count = 0;
            
            
            foreach (var item in query)
            {
                var=(item.imagePath);
                int len = var.Length;
                int start = len - 29;
                //string newPath = null;
                char[] newpath = new char[40];
                int tempcounter = 0;
                for (int i = 39; i >= 0; i--)
                {
                    if (var[len-1] == '\\')
                    {
                        newpath[i] = '/';
                        tempcounter++;
                    }
                    else if (var[len - 1] == 'C')
                    {
                        newpath[i] = 'C';
                        tempcounter++;
                        break;
                    }
                    else
                    {
                        newpath[i] = var[len - 1];
                        tempcounter++;
                    }
                    len--;
                }

                char[] newTempPath = new char[tempcounter];
                int newCOunter = 0;
                for (int i=0;i<newpath.Length;i++)
                {
                    if (newpath[i] != '\0')
                    {
                        newTempPath[newCOunter] = newpath[i];
                        newCOunter++;
                    }
                }
                string s = string.Join("", newTempPath);
                
                newpath = null;
                HouseImages copyDatabase = new HouseImages();
                copyDatabase.imageURL = s;
                data.Add(copyDatabase);
            }
            //int len = var.Length;
            return View(data);
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(User u)
        {
            Boolean flag = false;
            List<User> userList = userDb.Users.ToList<User>();
            foreach (User item in userList)
            {
                if (u.email==item.email &&u.password==item.password)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                return View("home");
            }
            ViewBag.error = "Email or password not match";
            return View("Login");
            
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            Boolean flag = true;
            string str = user.password;
            List<User> userList = userDb.Users.ToList<User>();
            ViewBag.str1 = "Email is already exist";
            foreach (User item in userList)
            {
                if (user.email == item.email && user.password == item.password)
                {
                    flag = false;
                    return View("SignUp");
                }
            }
            if (flag)
            {
                userDb.Users.Add(user);
                userDb.SaveChanges();
                
            }

            return View("Login");
        }
        public ActionResult ViewMaterialRates()
        {
            List<MaterialRate> data = new List<MaterialRate>();
            var obj = Dbmaterials.MaterialRates.First(a => a.standard == "Low");
            var obj2 = Dbmaterials.MaterialRates.First(a => a.standard == "Medimum");
            var obj3 = Dbmaterials.MaterialRates.First(a => a.standard == "High");
            data.Add(obj);
            data.Add(obj2);
            data.Add(obj3);
            return View(data);
        }
        public ActionResult WallInputs()
        {
            return View();
        }
        public ActionResult ManualInputs()
        {
            return View();
        }
        public ActionResult CalculateHouse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CalculateHouse(Materials obj)
        {
            /*obj.Roomthickness = obj.Roomthickness / 12;
            double area = obj.RoomHeigth * obj.RoomLength * obj.Roomthickness;
            obj.bricks = area * 13.5;
            double temp1 = area * 0.3;
            obj.cement = temp1 / 4;
            obj.sand = obj.cement * 3;
                //Now because two walls are same in each room
            obj.bricks = obj.bricks * 2;
            obj.cement = obj.cement * 2;
            obj.sand = obj.sand * 2;

            //Now for other two walls
            double area1 = obj.RoomHeigth * obj.RoomWidth * obj.Roomthickness;
            double b1 = area1 * 13.5;
            double temp2 = area1 * 0.3;
            double c1 = temp2 / 4;
            double s1 = obj.cement * 3;
            //Now because two walls are same in each room
            b1 = b1 * 2;
            c1 = c1 * 2;
            s1 = s1 * 2;

            //Now add material for all walls
            obj.bricks = obj.bricks + b1;
            obj.cement = obj.cement + c1;
            obj.sand = obj.sand + s1;

            //Now multiply this material with total rooms
            obj.bricks = obj.bricks * obj.RoomCount;
            obj.cement = obj.cement * obj.RoomCount;
            obj.sand = obj.sand * obj.RoomCount;

            //Now do this for plaster
            double plaster = obj.RoomHeigth * obj.RoomLength * 0.41 * 1.27;
            double c2 = plaster / 4;
            double s2 = ((plaster / 4) * 3);
            c2 = c2 * 2;
            s2 = s2 * 2;
            //Now for other two walls
            double plaster1 = obj.RoomHeigth * obj.RoomWidth * 0.41 * 1.27;
            double c3 = plaster1 / 4;
            double s3 = ((plaster1 / 4) * 3);
            c3 = c3 * 2;
            s3 = s3 * 2;
            //Now total material for plaster
            double c4 = (c2 + c3) * obj.RoomCount;
            double s4 = (s2 + s3) * obj.RoomCount;
            //Now total material for all the rooms
            obj.cement = obj.cement + c4;
            obj.sand = obj.sand + s4;
            obj.cement = obj.cement /1.25;*/
            obj.Roomthickness = obj.Roomthickness / 12;
            int rat = obj.ratio;
            double area = obj.RoomHeigth * obj.RoomLength * obj.Roomthickness;
            double area1 = obj.RoomHeigth * obj.RoomWidth * obj.Roomthickness;
            double areadoor = obj.DoorHeight * obj.DoorWidth * obj.Roomthickness;
            area = area * 2;
            area1 = area1 * 2;
            double area2 = area + area1 - areadoor;
            obj.bricks = area2 * 13.5;
            double temp1 = area2 * 0.3;
            obj.cement = temp1 / rat;
            obj.sand = obj.cement * (rat - 1);
            //For plaster
            double plaster1 = obj.RoomHeigth * obj.RoomLength * 0.41 * 1.27;
            double plaster2 = obj.RoomHeigth * obj.RoomWidth * 0.41 * 1.27;
            double doorplaster = obj.DoorHeight * obj.DoorWidth * 0.41 * 1.27;
            plaster1 = plaster1 * 2;
            plaster2 = plaster2 * 2;
            double plaster = plaster1 + plaster2 - doorplaster;
            obj.cement = obj.cement + (plaster / rat);
            obj.sand = obj.sand + ((plaster / rat) * (rat - 1));
            //For Lanter
            obj.Roofthickness = obj.Roofthickness / 12;
            double area4 = obj.RoomWidth * obj.RoomLength * obj.Roofthickness;
            double temp4 = area4 * 1.54;
            obj.cement = obj.cement + (temp4 / 7);
            obj.sand = obj.sand + (obj.cement * 2);
            obj.crush = obj.cement * 4;
            obj.steel = 3.06;
            obj.steel = obj.steel * area4;
            //Multiply with total rooms
            obj.bricks = obj.bricks * obj.RoomCount;
            obj.cement = obj.cement * obj.RoomCount;
            obj.sand = obj.sand * obj.RoomCount;
            obj.crush = obj.crush * obj.RoomCount;
            obj.steel = obj.steel * obj.RoomCount;

            //Now for the BathRooms
            obj.BathRoomthickness = obj.BathRoomthickness / 12;
            double batharea = obj.BathRoomHeight * obj.BathRoomLength * obj.BathRoomthickness;
            double batharea1 = obj.BathRoomHeight * obj.BathRoomWidth * obj.BathRoomthickness;
            double Bareadoor = obj.BathDoorHeight * obj.BathDoorWidth * obj.BathRoomthickness;
            batharea = batharea * 2;
            batharea1 = batharea1 * 2;
            double batharea2 = batharea + batharea1 - Bareadoor;
            double bathbricks = batharea2 * 13.5;
            double bathtemp1 = batharea2 * 0.3;
            double bathcement = bathtemp1 / rat;
            double bathsand = bathcement * (rat - 1);
            //For plaster
            double bathplaster1 = obj.BathRoomHeight * obj.BathRoomLength * 0.41 * 1.27;
            double bathplaster2 = obj.BathRoomHeight * obj.BathRoomWidth * 0.41 * 1.27;
            double Bdoorplaster = obj.BathDoorHeight * obj.BathDoorWidth * 0.41 * 1.27;
            bathplaster1 = bathplaster1 * 2;
            bathplaster2 = bathplaster2 * 2;
            double bathplaster = bathplaster1 + bathplaster2 - Bdoorplaster;
            bathcement = bathcement + (bathplaster / rat);
            bathsand = bathsand + ((bathplaster / rat) * (rat - 1));
            //For Lanter
            double batharea4 = obj.BathRoomWidth * obj.BathRoomLength * obj.Roofthickness;
            double bathtemp4 = batharea4 * 1.54;
            bathcement = bathcement + (bathtemp4 / 7);
            bathsand = bathsand + (bathcement * 2);
            double bathcrush = bathcement * 4;
            double bathsteel = 3.06;
            bathsteel = bathsteel * batharea4;
            //Now for total baths
            bathbricks = bathbricks * obj.BathRoomCount;
            bathcement = bathcement * obj.BathRoomCount;
            bathsand = bathsand * obj.BathRoomCount;
            bathcrush = bathcrush * obj.BathRoomCount;
            bathsteel = bathsteel * obj.BathRoomCount;

            //Now for Kitchen
            obj.Kitchenthickness = obj.Kitchenthickness / 12;
            double karea = obj.KitchenHeight * obj.KitchenLength * obj.Kitchenthickness;
            double karea1 = obj.KitchenHeight * obj.KitchenWidth * obj.Kitchenthickness;
            double Kareadoor = obj.KitchenDoorHeight * obj.KitchenDoorWidth * obj.Kitchenthickness;
            karea = karea * 2;
            karea1 = karea1 * 2;
            double karea2 = karea + karea1 - Kareadoor;
            double kbricks = karea2 * 13.5;
            double ktemp1 = karea2 * 0.3;
            double kcement = ktemp1 / rat;
            double ksand = kcement * (rat - 1);
            //For plaster
            double kplaster1 = obj.KitchenHeight * obj.KitchenLength * 0.41 * 1.27;
            double kplaster2 = obj.KitchenHeight * obj.KitchenWidth * 0.41 * 1.27;
            double Kdoorplaster = obj.KitchenDoorHeight * obj.KitchenDoorWidth * 0.41 * 1.27;
            kplaster1 = kplaster1 * 2;
            kplaster2 = kplaster2 * 2;
            double kplaster = kplaster1 + kplaster2 - Kdoorplaster;
            kcement = kcement + (kplaster / rat);
            ksand = ksand + ((kplaster / rat) * (rat - 1));
            //For Lanter
            double karea4 = obj.KitchenWidth * obj.KitchenLength * obj.Roofthickness;
            double ktemp4 = karea4 * 1.54;
            kcement = kcement + (ktemp4 / 7);
            ksand = ksand + (kcement * 2);
            double kcrush = kcement * 4;
            double ksteel = 3.06;
            ksteel = ksteel * karea4;
            //Now for total kitchens
            kbricks = kbricks * obj.KitchenCount;
            kcement = kcement * obj.KitchenCount;
            ksand = ksand * obj.KitchenCount;
            kcrush = kcrush * obj.KitchenCount;
            ksteel = ksteel * obj.KitchenCount;

            //NOW FOR TOTAL MATERIAL

            obj.bricks = obj.bricks + bathbricks + kbricks;
            obj.cement = obj.cement + bathcement + kcement;
            obj.sand = obj.sand + bathsand + ksand;
            obj.crush = obj.crush + bathcrush + kcrush;
            obj.steel = obj.steel + bathsteel + ksteel;
            //obj.steel = obj.BathRoomCount;
            obj.cement = obj.cement / 1.25;

            List<Materials> list = new List<Materials>();
            list.Add(obj);
            return View(list);
        }
        public ActionResult Calculate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Calculate(Materials obj)
        {
            obj.Wallthickness = obj.Wallthickness / 12;
            int rat = obj.ratio;
            double area = obj.WallHeigth * obj.WallLength * obj.Wallthickness;
            obj.bricks = area * 13.5;
            double temp1 = area * 0.3;
            obj.cement = temp1 / rat;
            obj.sand = obj.cement * (rat - 1);
            //For plaster

            double plaster = obj.WallHeigth * obj.WallLength * 0.41 * 1.27;
            obj.cement = obj.cement + (plaster / rat);
            obj.cement = obj.cement / 1.25;

            obj.sand = obj.sand + ((plaster / rat) * (rat - 1));
            List<Materials> list = new List<Materials>();
            list.Add(obj);
            return View(list);
        }
        [HttpPost]
        public ActionResult CalculateConcrete(Materials obj)
        {
            obj.steel = 3.06;
            obj.Wallthickness = obj.Wallthickness / 12;
            double area = obj.WallHeigth * obj.WallLength * obj.Wallthickness;
            double temp1 = area * 1.54;
            obj.cement = temp1 / 7;
            obj.sand = obj.cement * 2;
            //For plaster

            obj.crush = obj.cement * 4;
            obj.cement = obj.cement / 1.25;
            obj.steel = obj.steel * area;
            List<Materials> list = new List<Materials>();
            list.Add(obj);
            return View(list);
        }
        public ActionResult Contactus()
        {
            List<Organizer> data = new List<Organizer>();
            data = db.Organizers.ToList();
            return View("Sent_Email");

        }
        //public ActionResult ManualInputs()
        //{
        //    return View();
        //}
        //public ActionResult Calculate()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Calculate(Materials obj)
        //{
        //    obj.Wallthickness = obj.Wallthickness / 12;
        //    double area = obj.WallHeigth * obj.WallLength * obj.Wallthickness;
        //    obj.bricks = area * 13.5;
        //    double temp1 = area * 0.3;
        //    obj.cement = temp1 / 4;
        //    obj.sand = obj.cement * 3;
        //    //For plaster

        //    double plaster = obj.WallHeigth * obj.WallLength * 0.41 * 1.27;
        //    obj.cement = obj.cement + (plaster / 4);
        //    obj.cement = obj.cement / 1.25;

        //    obj.sand = obj.sand + ((plaster / 4) * 3);
        //    List<Materials> list = new List<Materials>();
        //    list.Add(obj);
        //    return View(list);
        //}
        //[HttpPost]
        //public ActionResult CalculateConcrete(Materials obj)
        //{
        //    obj.steel = 3.06;
        //    obj.Wallthickness = obj.Wallthickness / 12;
        //    double area = obj.WallHeigth * obj.WallLength * obj.Wallthickness;
        //    double temp1 = area * 1.54;
        //    obj.cement = temp1 / 7;
        //    obj.sand = obj.cement * 2;
        //    //For plaster

        //    obj.crush = obj.cement * 4;
        //    obj.cement = obj.cement / 1.25;
        //    obj.steel = obj.steel * area;
        //    List<Materials> list = new List<Materials>();
        //    list.Add(obj);
        //    return View(list);
        //}
        //[HttpPost]
        public ActionResult RoomInputs()
        {
            return View();
        }
        public ActionResult CalculateRoom()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CalculateRoom(Materials obj)
        {
            //obj.steel = obj.RoomWidth;
            obj.Roomthickness = obj.Roomthickness / 12;
            int rat = obj.ratio;
            double area = obj.RoomHeigth * obj.RoomLength * obj.Roomthickness;
            double area1 = obj.RoomHeigth * obj.RoomWidth * obj.Roomthickness;
            double areadoor = obj.DoorHeight * obj.DoorWidth * obj.Roomthickness;
            area = area * 2;
            area1 = area1 * 2;
            double area2 = area + area1;
            obj.bricks = area2 * 13.5;
            double doorBrick = areadoor * 13.5;
            obj.bricks = obj.bricks - doorBrick;
            area2 = area2 - areadoor;
            double temp1 = area2 * 0.3;
            obj.cement = temp1 / rat;
            obj.sand = obj.cement * (rat-1);
            //For plaster

            double plaster1 = obj.RoomHeigth * obj.RoomLength * 0.41 * 1.27;
            double plaster2= obj.RoomHeigth * obj.RoomWidth * 0.41 * 1.27;
            double doorplaster = obj.DoorHeight * obj.DoorWidth * 0.41 * 1.27;
            plaster1 = plaster1 * 2;
            plaster2 = plaster2 * 2;
            double plaster = plaster1 + plaster2 - doorplaster;
            obj.cement = obj.cement + (plaster / rat);
            //obj.cement = obj.cement / 1.25;
            obj.sand = obj.sand + ((plaster / rat) * (rat - 1));

            obj.Roofthickness = obj.Roofthickness / 12;
            double area4 = obj.RoomWidth * obj.RoomLength * obj.Roofthickness;
            double temp4 = area4 * 1.54;
            obj.cement = obj.cement + temp4 / 7;
            obj.sand = obj.sand + (obj.cement * 2);
            obj.crush = obj.cement * 4;
            obj.steel = 3.06;
            obj.steel = obj.steel * area4;
            obj.cement = obj.cement / 1.25;

            List<Materials> list = new List<Materials>();
            list.Add(obj);
            return View(list);
        }
    
        public ActionResult MapUpdate(HttpPostedFileBase files)
        {

            SaveImage("file1");
            return View();

        }
        private void SaveImage(string file)
        {
            HttpPostedFileBase file1 = Request.Files[file];
            var fileName = Path.GetFileName(file1.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/mapImg/"), fileName);
            
            
            file1.SaveAs(path);
        }
        
    }

}
