using Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SunService.Domain.AppServices.SunServices.BaseEntities;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.AppServices.SunServices.UserS;
using SunService.Domain.Core.SunServices.BaseEntities.AppServices;
using SunService.Domain.Core.SunServices.BaseEntities.Data;
using SunService.Domain.Core.SunServices.BaseEntities.Services;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.Data;
using SunService.Domain.Core.SunServices.HService.Services;
using SunService.Domain.Core.SunServices.UserS.AppServices;
using SunService.Domain.Core.SunServices.UserS.Data;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Services;
using SunService.Domain.Core.Task.Configs;
using SunService.Domain.Services.SunServices.BaseEntities;
using SunService.Domain.Services.SunServices.HService;
using SunService.Domain.Services.SunServices.UserS;
using SunService.EndPoints.Mvc.Task.Middleware;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using SunService.Infra.Data.Repos.Ef.SunServices.BaseEntities;
using SunService.Infra.Data.Repos.Ef.SunServices.HService;
using SunService.Infra.Data.Repos.Ef.SunServices.UserS;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(o =>
{
    o.ClearProviders();
    o.AddSerilog();
}).UseSerilog((context, config) =>
{
    config.WriteTo.Console();
    config.WriteTo.Seq("http://localhost:5341/", apiKey: "G3kSqNTR3g9VgOAwKZdd");
});

// Add services to the container.

#region Configuration

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
builder.Services.AddSingleton(siteSettings);

#endregion
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<User, IdentityRole<int>>(option =>
{
    option.SignIn.RequireConfirmedAccount = false;
    option.Password.RequireDigit = false;
    option.Password.RequiredLength = 6;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
   
    option.User.RequireUniqueEmail = true;

})

   .AddRoles<IdentityRole<int>>()
   .AddErrorDescriber<PersianIdentityErrorDescriber>()
   .AddEntityFrameworkStores<AppDbContext>();


#region Register Services
builder.Services.AddScoped<IBaseEntitiesRepository, BaseEntitiesRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IHomeServiceRepository, HomeServiceRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IorderRepository, orderRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IBaseEntitiesServices, BaseEntitiesServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();
builder.Services.AddScoped<IHomeServiceServices, HomeServiceServices>();
builder.Services.AddScoped<IOfferServices, OfferServices>();
builder.Services.AddScoped<IorderServices, orderServices>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IExpertServices, ExpertServices>();
builder.Services.AddScoped<IRatingServices, RatingServices>();
builder.Services.AddScoped<IUserSAppServices, UserSAppServices>();
builder.Services.AddScoped<IBaseDataAppService, BaseDataAppService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();
builder.Services.AddScoped<ICategoryAppServices, CategoryAppServices>();
builder.Services.AddScoped<ISubCategoryAppServices, SubCategoryAppServices>();
builder.Services.AddScoped<IHomeServiceAppServices, HomeServiceAppServices>();
builder.Services.AddScoped<IHomeServiceServices, HomeServiceServices>();
builder.Services.AddScoped<IHomeServiceRepository, HomeServiceRepository>();
builder.Services.AddScoped<IorderAppServices, orderAppServices>();
builder.Services.AddScoped<IOfferAppServices, OfferAppServices>();
builder.Services.AddScoped<IRatingAppServices, RatingAppServices>();
builder.Services.AddScoped<IGetStatisticsDataAppServices, GetStatisticsDataAppServices>();
builder.Services.AddScoped<IGetStatisticsDataReopsitory, GetStatisticsDataReopsitory>();
builder.Services.AddScoped<IGetStatisticsDataServices, GetStatisticsDataServices>();



builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(siteSettings.ConnectionStrings.SqlConnection));
#endregion
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/UserS/Login";
    options.AccessDeniedPath = "/Account/UserS/AccessDenied";

});
builder.Services.AddMemoryCache();
var app = builder.Build();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".svg"] = "image/svg+xml";
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
//app.UseErrorLogging();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
