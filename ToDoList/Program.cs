using Microsoft.EntityFrameworkCore;
using DatabaseAccessLayer.Data;
using BusinessLogicLayer.Repository.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using DatabaseAccessLayer.Utility;
using DatabaseAccessLayer.Repos;
using ToDoList.Settings;
using SendGrid.Extensions.DependencyInjection;
using ToDoList.ServicesGetEmailSender;
using BusinessLogicLayer.Repository.Interfaces;
using DatabaseAccessLayer.Interface;


namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IMyTaskService,MyTaskService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IStatusService,StatusService>();
            builder.Services.AddScoped<IStatusRepo,StatusRepo>();
            builder.Services.AddScoped<IMyTaskRepo,MyTaskRepo>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>{
                config.SignIn.RequireConfirmedEmail = true;
                config.SignIn.RequireConfirmedAccount = true;
                config.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGridSettings"));

            builder.Services.AddRazorPages();
            builder.Services.AddSendGrid(options =>
            {
                options.ApiKey = builder.Configuration.GetSection("SendGridSettings").GetValue<string>("ApiKey");
            });

            builder.Services.AddScoped<IEmailSender,EmailSenderService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
             app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=MyTasks}/{action=Index}/{id?}");
        //   pattern: "{controller=MyTasks}/{action=Index}/{id?}");
            app.Run();
        }
    }
}