

using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Core.CommentService;
using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Core.LectureResourceService;
using GalacticUniversity.Core.LectureService;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Core.Services;
using GalacticUniversity.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GalacticUniversity.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using GalacticUniversity.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using GalacticUniversity.Models;
using GalacticUniversity.Core.CloudinaryService;
using CloudinaryDotNet;
using GalacticUniversity.Core.UserCourseService;
using GalacticUniversity.Core.UserService;
using GalacticUniversity.DataAccess.UserRepository;
using GalacticUniversity.Core.TarotCardService;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),b=>b.MigrationsAssembly("GalacticUniversity.DataAccess")));



builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

/*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();*/

builder.Services.AddScoped<CloudinaryService>();


var cloudinarySetting = builder.Configuration.GetSection("Cloudinary").Get<CloudinarySettings>();

var account = new Account(cloudinarySetting.CloudName, cloudinarySetting.ApiKey, cloudinarySetting.ApiSecret);

var cloudinary = new Cloudinary(account);
builder.Services.AddSingleton(cloudinary);

builder.Services.AddScoped<TarotService>();





builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IUserRepository<User>, UserRepository<User>>();



builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICourseService,CourseService>();
builder.Services.AddScoped<ILectureResourceService,LectureResourceService>();
builder.Services.AddScoped<ILectureService, LectureService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUserCourseService, UserCourseService>();
builder.Services.AddScoped<IUserService<User>, UserService>();



/*builder.Services.AddDefaultIdentity<IdentityUser>(options=>options.SignIn.RequireConfirmedAccount=true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();*/

builder.Services.AddRazorPages();

/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                                                                                                options.LoginPath = "/Account/Login");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.AccessDeniedPath = "/Account/AccessDenied");
                                                                                                
                                                                                                */

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
