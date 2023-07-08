using MagicVilla_Web.Services;
using MagicVilla_Web.Services.IServices;
using MagicVilla_Wep;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingConfig));
var app = builder.Build();

builder.Services.AddHttpClient<IVillaService, VillaService>();
// AddScoped mean when we want create an obj from any class we will call the same obj
builder.Services.AddScoped<IVillaService, VillaService>();

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

