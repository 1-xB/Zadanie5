using Zadanie5.Data;
using Zadanie5.Infrastructure.Repositories;
using Zadanie5.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNpgsql<DatabaseContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPeselValidator, PeselValidator>();

builder.Services.AddScoped<FileProcessingService>();
builder.Services.AddScoped<FileCreatingService>();

builder.Services.AddScoped<IKlientRepository, KlientRepository>();
builder.Services.AddScoped<IKlientService, KlientService>();
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
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();