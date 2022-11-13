using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
// AGREGAR COOCKIE
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Auth/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Home/Denegado";
    });
// FIN COOCKIE
builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30)); // AGREGAR TIEMPO EN LA SESION
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


// INICIO GLOBALIZACION DECIMAL
var supportedCultures = new[] { "en-US"};
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
// FIN GLOBALIZACION DECIMAL

// INICIO HABILITAR SESION
app.UseSession();
// FIN HABILITAR SESION


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();// AGREGAR COOCKIE
app.UseAuthorization();// AGREGAR COOCKIE


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
