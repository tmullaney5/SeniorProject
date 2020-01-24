using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SightWordsProject.Models;
using SightWordsProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SightWordsProject.Utilities
{
    public class ValidAccessCodeAttribute : ValidationAttribute
    {
        //private readonly IConfiguration configuration;
        //private string connectionString;
        DbContextOptionsBuilder<AppDbContext> optionsBuilder;

        // public ValidAccessCodeAttribute()
        // {
        //     optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        //     connectionString = configuration.GetConnectionString("DefaultConnection");
        //     optionsBuilder.UseSqlServer("Server=tcp:sightwordsserver.database.windows.net,1433;Initial Catalog=SightWordsDatabase;Persist Security Info=False;User ID=team9;Password=sightwords#9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        // }
        public override bool IsValid(object value)
        {
            optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=tcp:sightwordsserver.database.windows.net,1433;Initial Catalog=SightWordsDatabase;Persist Security Info=False;User ID=team9;Password=sightwords#9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            using (AppDbContext context = new AppDbContext(optionsBuilder.Options))
            {
                string studentCode = value.ToString();
                bool code = context.TeacherLogin.Any(x => x.StudentCode == studentCode);
                return base.IsValid(code);
            }
        }
    }
}