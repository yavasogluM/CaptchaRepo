using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Captcha.Web.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;

namespace Captcha.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Captcha.Helper.ICaptchaHelper _captchaHelper;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, Captcha.Helper.ICaptchaHelper captchaHelper, IWebHostEnvironment env)
        {
            _logger = logger;
            _captchaHelper = captchaHelper;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<FileResult> CaptchaImage()
        {
            try
            {
                var captcha = await _captchaHelper.GenerateForSumQuestionCaptcha();

                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddMinutes(60); //depends to form filling time 
                Response.Cookies.Append("CAPTCHAVALUE", captcha.CaptchaValue, cookie);

                string fileName = Helper.DrawHelper.DrawImage(captcha.CaptchaText, _env.ContentRootPath + "/wwwroot/Uploads/");
                return File($"/Uploads/{fileName}", "image/jpeg");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IActionResult> TestForm()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> TestForm(string value)
        {
            string cookieCaptchaValue = Request.Cookies["CAPTCHAVALUE"];

            bool captchaControl = await _captchaHelper.CaptchaIsValid(value, cookieCaptchaValue);

            ViewBag.CaptchaControl = captchaControl;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
