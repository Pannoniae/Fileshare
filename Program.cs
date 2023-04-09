using Fileshare.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDirectoryBrowser();
builder.Services.AddSingleton<WeatherForecastService>();
builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

app.UseDeveloperExceptionPage();

var fileProvider = new PhysicalFileProvider(@"G:\music");
var requestPath = "/share";
app.UseStaticFiles();
// Enable displaying browser links.
app.UseStaticFiles(new StaticFileOptions {
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseFileServer(new FileServerOptions {
    StaticFileOptions = {
        ServeUnknownFileTypes = true,
        HttpsCompression = HttpsCompressionMode.DoNotCompress
    },
    DirectoryBrowserOptions = {
        FileProvider = fileProvider,
        RequestPath = requestPath
    },
    FileProvider = fileProvider,
    RequestPath = requestPath,
    EnableDirectoryBrowsing = true
});
app.MapControllers();
app.MapDefaultControllerRoute();
app.MapRazorPages();
app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");

app.Run();