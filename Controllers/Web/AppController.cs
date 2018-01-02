using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers.Web
{
    public class AppController :Controller
    {
        private IMailService _mailservice;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;


        //private WorldContext _context;

        public AppController(IMailService mailservice, IConfigurationRoot config,IWorldRepository repository,ILogger<AppController> logger)
        {
            _mailservice = mailservice;
            _config = config;
            _repository = repository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            //try
            //{
            //    var data = _repository.GetAllTrips();
            //    return View(data);
            //}
            //catch(Exception ex)
            //{
            //    _logger.LogError($"Failed to get trips in Inedx page:{ex.Message}");
            //    return Redirect("/error");
            //}
            return View();
        }
        [Authorize]
        public IActionResult Trips()
        {
            //try
            //{
            //    var trips = _repository.GetAllTrips();
            //    return View(trips);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Failed to get trips in Inedx page:{ex.Message}");
            //    return Redirect("/error");
            //}
            return View();
        }

        public IActionResult About()
        {

            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {

            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("", "Aol is not supported");
            }
            if (ModelState.IsValid)
            {
                _mailservice.SendMail(_config["MailSettings:ToAddress"], model.Email, "From The World", model.Message);

                ModelState.Clear();
                ViewBag.UserMessage = "Message sent";
            }
            return View();
        }
    }
}
