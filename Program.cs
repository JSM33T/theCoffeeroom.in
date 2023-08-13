using Serilog;
using theCoffeeroom.Interfaces;
using theCoffeeroom.Repositories;
using theCoffeeroom.Services;
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
builder.Services.AddScoped<IDataAccessRepo, DataAccessRepo>();
//builder.Services.AddScoped<ViewRenderService>();


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
    options.MinificationSettings.MinifyEmbeddedCssCode = true;
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
else
{
    app.UseExceptionHandler("/Home/PublicError");
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseWebMarkupMin();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
