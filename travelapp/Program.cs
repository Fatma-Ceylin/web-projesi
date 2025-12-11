using Microsoft.EntityFrameworkCore; // Gerekli kütüphane
using travelapp.Data;                // Gerekli kütüphane

var builder = WebApplication.CreateBuilder(args);

// --- SERVÝS AYARLARI BURADA BAÞLAR ---

// Add services to the container.
builder.Services.AddControllersWithViews();

// Program.cs içinde olmalý:
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// --- SERVÝS AYARLARI BURADA BÝTER ---


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

// Authentication/Authorization
// Eðer kimlik doðrulama (login) kullanýyorsan, UseAuthentication ve UseAuthorization sýrasý önemlidir.
// UseAuthentication() Identity kullanýyorsan buraya eklenmeli.
// builder.Services.AddIdentity... eklediysen, app.UseRouting() ve app.UseAuthorization() arasýna eklenir.

app.UseAuthorization(); // Bu kýsým zaten var

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();