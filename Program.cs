using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Session;
using Serilog;
using theCoffeeroom.Services.Helpers;
using WebMarkupMin.AspNetCore7;
using WebMarkupMin.Core;

var builder = WebApplication.CreateBuilder(args);

//registering services
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".coffeebreak.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
});

//DA service
//builder.Services.AddScoped<IDataAccessRepo, DataAccessRepo>();
//builder.Services.AddScoped<IAvatarRepo, AvatarRepo>();
//builder.Services.AddSingleton<SqlService>(provider =>
//{
//    string connectionString = ConfigHelper.NewConnectionString.ToString();
//    return new SqlService(connectionString);
//});
builder.Services.AddSingleton<SqlService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.Cookie.HttpOnly = true;
           options.Cookie.IsEssential = true;
           options.ExpireTimeSpan = TimeSpan.FromDays(100);
           options.SlidingExpiration = true; // Extend the expiration time with each request
       });

builder.Services.AddWebMarkupMin(options =>
{
    options.AllowMinificationInDevelopmentEnvironment = true;
    options.AllowCompressionInDevelopmentEnvironment = true;
})
.AddHtmlMinification(options =>
{
    options.MinificationSettings.PreserveNewLines = false;
    options.MinificationSettings.MinifyEmbeddedCssCode = true;
    options.MinificationSettings.RemoveHtmlComments = true;
    options.MinificationSettings.WhitespaceMinificationMode = WhitespaceMinificationMode.Safe;
    options.MinificationSettings.RemoveHtmlCommentsFromScriptsAndStyles = true;
    options.MinificationSettings.MinifyEmbeddedJsCode = true;
})
.AddXmlMinification()
.AddHttpCompression();

var app = builder.Build();

//logger de serilog (console + file sink)
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Async(writeTo => writeTo.Console())
                .WriteTo.Async(a => a.File("Logs/coffeelog.txt", rollingInterval: RollingInterval.Day))
                .CreateLogger();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseMiddleware<SessionMiddleware>();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseWebMarkupMin();
app.UseRouting();
app.UseAuthorization();
app.UseCookieCheckMiddleware();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapFallbackToFile("index.php");
app.Run();
