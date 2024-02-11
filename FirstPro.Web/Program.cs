using FirstPro.BLL.Mapper;
using FirstPro.BLL.Service.Interface;
using FirstPro.BLL.Service.Repoistory;
using FirstPro.DAL.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);





// Add services to the container.
builder.Services.AddControllersWithViews();

#region connectionstring
var connectionstring = builder.Configuration.GetConnectionString("ApplicatonConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionstring));
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
#endregion

// Assuming you have a generic repository like GenericRepository<TEntity>
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Assuming IDepartmentService is your service interface and DepartmentService is the implementation
builder.Services.AddScoped<IDepartmentService,DepartmentServic>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


//builder.Services.AddScoped<IDepartmentService, DepartmentService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
