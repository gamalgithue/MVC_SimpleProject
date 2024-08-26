using FirstPro.BLL.Helper;
using FirstPro.BLL.Mapper;
using FirstPro.BLL.Service.Interface;
using FirstPro.BLL.Service.Repoistory;
using FirstPro.DAL.Database;
using FirstPro.DAL.Extend;
using FirstPro.Web.Languages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);





// Add services to the container.
builder.Services.AddControllersWithViews().
    AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
    }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(SharedResource));
});




#region connectionstring
var connectionstring = builder.Configuration.GetConnectionString("ApplicatonConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionstring));
#endregion



#region AutoMapper
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
#endregion


#region services
// Assuming you have a generic repository like GenericRepository<TEntity>
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Assuming IDepartmentService is your service interface and DepartmentService is the implementation
builder.Services.AddScoped<IDepartmentService,DepartmentServic>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<MailSender>();


#endregion







#region Microsoft Identity Configuration


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
            options =>
            {
           options.LoginPath = new PathString("/Account/Login");
            options.AccessDeniedPath = new PathString("/Account/Login");
         });
                builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                 .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
             builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
          {

              options.User.RequireUniqueEmail = true;
              options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789@_-.+";
           // Default Password settings.
             options.Password.RequireDigit = true;
             options.Password.RequireLowercase = true;
              options.Password.RequireNonAlphanumeric = true;
               options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
              }).AddEntityFrameworkStores<ApplicationDbContext>();


#endregion


#region GoogleAuthentication


builder.Services.AddAuthentication()
    .AddGoogle(
    options =>
    {
        options.ClientId = builder.Configuration["ExternalLogins:Google:ClientId"];
        options.ClientSecret = builder.Configuration["ExternalLogins:Google:ClientSecret"];
    })
    .AddFacebook(
    options =>
    {
        options.AppId = builder.Configuration["ExternalLogins:Facebook:AppId"];
        options.AppSecret = builder.Configuration["ExternalLogins:Facebook:AppSecret"];
    }
    );
#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();
