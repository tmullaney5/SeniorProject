using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SightWordsProject.Models;
using SightWordsProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
//using MailKit.Net.Smtp;
//using MimeKit;

namespace SightWordsProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private string connectionString;
        DbContextOptionsBuilder<AppDbContext> optionsBuilder;
        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);


        public TeacherController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            configuration = config;
            optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult TeacherLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TeacherLogin(TeacherLoginVM model)
        {
            if(ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                ApplicationUser user = new ApplicationUser();
                AppDbContext context = new AppDbContext(optionsBuilder.Options);
                user = context.TeacherLogin.Single(x => x.UserName == model.Email);
                if(result.Succeeded)
                {
                    return RedirectToAction("TeacherDashboard", "Teacher");
                }
                
                ModelState.AddModelError(string.Empty, "invalid login attempt");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateAccount()
        {

            return View("Views/Teacher/CreateAccount.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountVM model)
        {
            if(ModelState.IsValid)
            {
                model.StudentCode = GenerateStudentCode();
                var user = new ApplicationUser 
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    School = model.School,
                    UserName = model.Email, 
                    Email = model.Email, 
                    StudentCode = model.StudentCode,
                    UserType = "teacher",
                    GradeLevel = model.GradeLevel
                };
                var result = await userManager.CreateAsync(user,model.Password);


                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                MailMessage mailmsg = new MailMessage();

                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("sightwordsrock@gmail.com","Sightwords1!");

                mailmsg.From = new MailAddress("sightwordsrock@gmail.com");
                mailmsg.To.Add(user.Email);

                mailmsg.Body = "<h3> To sign into your account use the credentials you used to create an account. To add students to your class use the code: </h3>" + user.StudentCode;
                mailmsg.Subject = "Account Created for Sight Words Rock";
                mailmsg.IsBodyHtml = true;

                mailmsg.Priority = MailPriority.High;

                try
                {
                    smtp.Timeout = 500000;
                    smtp.Send(mailmsg);
                    mailmsg.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            
                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("TeacherDashboard", "Teacher");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> TeacherDashboard()
        {
            var user = await GetCurrentUserAsync();
            return View(user);
        }

        [HttpGet]
        public IActionResult ViewClass(string id)
        {
            var user = GetUser(id);
            AppDbContext context = new AppDbContext(optionsBuilder.Options);
            var students = context.TeacherLogin.Where(x => x.StudentCode == user.StudentCode && x.UserType == "student").ToList();
            List<ViewClassVM> model = new List<ViewClassVM>();

            foreach(var student in students)
            {
                var item = new ViewClassVM
                {
                    StudentId = student.StudentId,
                    ParentEmail = student.Email,
                    ParentFirst = student.FirstName,
                    ParentLast = student.LastName,
                };
                model.Add(item);
            }

            return View(model);
        }
        public IActionResult Modules()
        {
            return View();
        }
        public IActionResult StudentStats()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {

            return View("Views/Teacher/ForgotPassword.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> ManageAccount(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            var model = new ManageUserVM()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                School = user.School,
                Email = user.Email,
                AccessCode = user.StudentCode,
                GradeLevel = user.GradeLevel
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageAccount(ManageUserVM model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.School = model.School;
            user.Email = model.Email;
            user.GradeLevel = model.GradeLevel;

            var result = await userManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                return RedirectToAction("TeacherDashboard");
            }

            foreach(var errors in result.Errors)
            {
                ModelState.AddModelError("",errors.Description);
            }
            return View(model);
        }

        public string GenerateStudentCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return(finalString);
        }
        public ApplicationUser GetUser(string id)
        {
            using (AppDbContext context = new AppDbContext(optionsBuilder.Options))
            {
                ApplicationUser user = context.TeacherLogin.Single(x => x.Id == id);
                return user;
            }
        }
    }
}