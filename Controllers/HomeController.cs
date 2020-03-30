using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomCodeGenerator.Models;
using Microsoft.AspNetCore.Http; //Session
using System.Text;

namespace RandomCodeGenerator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // If no session => set session count = 1
            if(HttpContext.Session.GetInt32("Count") == null)
            {
                int count = HttpContext.Session.GetInt32("Count").GetValueOrDefault();
                HttpContext.Session.SetInt32("Count", 1);
                ViewBag.Count += 1;
            }
            else
            {
                // if session exists => count + 1
                int count = HttpContext.Session.GetInt32("Count").GetValueOrDefault();
                HttpContext.Session.SetInt32("Count", count + 1);
                ViewBag.Count = count;
            }

            Random rand = new Random();
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var CharacterString = new char[14];
            for(int i = 0; i < CharacterString.Length; i++)
            {
                CharacterString[i] = characters[rand.Next(characters.Length)];
                
            }
            var FinalString = new String(CharacterString);
            ViewBag.GeneratedCode = FinalString;

            return View();
        }

        [HttpGet("reset")]
        public IActionResult Reset()
        {
            if(HttpContext.Session.GetInt32("Count") >= 1)
            {
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Index");
        }

    }
}
