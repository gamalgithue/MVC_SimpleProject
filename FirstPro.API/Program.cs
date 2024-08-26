using FirstPro.BLL.Helper;
using FirstPro.BLL.Mapper;
using FirstPro.BLL.Service.Interface;
using FirstPro.BLL.Service.Repoistory;
using FirstPro.DAL.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();





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
builder.Services.AddScoped<IDepartmentService, DepartmentServic>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<MailSender>();

#endregion



#region Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
