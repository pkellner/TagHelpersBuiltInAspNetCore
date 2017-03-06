using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TagHelpersBuiltInAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tooling()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        //[Route("/route1", Name = "route1")]
        //public IActionResult Route1()
        //{
        //    ViewData["Message"] = "Route1.";
        //    return View("");
        //}

        //[Route("/route2", Name = "route2")]
        //public IActionResult Route2()
        //{
        //    ViewData["Message"] = "Route2.";
        //    return View();
        //}



        public IActionResult Error()
        {
            return View();
        }
    }
}


//using Microsoft.AspNetCore.Mvc;

//namespace TagHelpersBuiltInAspNetCore.Controllers
//{
//    public class HomeController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Index(int number)
//        {
//            ViewData["Id"] = number.ToString();

//            return View();
//        }

//        public IActionResult About()
//        {
//            ViewData["Message"] = "Your application description page.";

//            return View();
//        }

//        public IActionResult NonSuckyYouTubeEmbed()
//        {
//            return View();
//        }

//        public IActionResult Sample()
//        {
//            return View();
//        }



//        public IActionResult Contact()
//        {
//            ViewData["Message"] = "Your contact page.";

//            return View();
//        }

//        public IActionResult Error()
//        {
//            return View();
//        }

//        public IActionResult AboutBlog()
//        {
//            return View();
//        }
//    }
//}
