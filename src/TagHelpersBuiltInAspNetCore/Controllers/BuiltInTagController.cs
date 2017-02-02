using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TagHelpersBuiltInAspNetCore.Controllers
{
    public class BuiltInTagController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AnchorTagHelper()
        {
            return View();
        }

        public IActionResult ImageTagHelper()
        {
            return View();
        }

        public IActionResult EnvironmentTagHelper()
        {
            return View();
        }

        public IActionResult CacheTagHelper(string id,string myParam1,string myParam2,string myParam3)
        {
            
            string viewName = id == null
                ? "CacheTagHelper/no-parameters"
                : "CacheTagHelper/" + id;



                int num1;
                int num2;
                int.TryParse(myParam1, out num1);
                int.TryParse(myParam2, out num2);
                return View(viewName, num1 + num2);
            

            //return View(viewName, myParam1 + myParam2);
        }

        

        //[Route("CacheTagHelper/{id:string}")]
        //public IActionResult CacheTagHelper(string id)
        //{
        //    string viewName = "CacheTagHelper/" + id;
        //    return View(viewName);
        //}
    }
}
