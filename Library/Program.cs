using DataLayer.Data;
using Hangfire;
using Library;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServiceLayer.Interface;
using ServiceLayer.Services;
using System.ComponentModel.Design;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBook, BookService>();
builder.Services.AddScoped<IShelf, ShelfService>();

builder.Services.AddLocalization(option => { option.ResourcesPath = "Resources"; });
builder.Services.AddMvc().AddViewLocalization(
    LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    var Cultures = new List<CultureInfo>
      {
          new CultureInfo("en"),
          new CultureInfo("fr"),
          new CultureInfo("es"),
      };
    option.DefaultRequestCulture = new RequestCulture("en");
    option.SupportedCultures = Cultures;
    option.SupportedUICultures = Cultures;
});

builder.Services.AddDbContext<LibraryDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:LibraryConnection"]);
});

builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration["ConnectionStrings:LibraryConnection"]));
builder.Services.AddHangfireServer();

// Add Job service
builder.Services.AddScoped<Job>();

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
app.UseHangfireDashboard(pathMatch:"/dash");
app.UseRequestLocalization(app.Services.
GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    recurringJobManager.AddOrUpdate<Job>(
        "my-recurring-job-id",
        job => job.Booklog(),
        "*/20 * * * * *"
    );
}
app.MapControllerRoute(
    name: "default",

pattern: "{controller=Shelves}/{action=Index}/{id?}");
app.Run();
