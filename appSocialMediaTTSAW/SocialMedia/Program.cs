using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


   
builder.Services.AddHttpClient("IdentityApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7082");

 });
builder.Services.AddHttpClient("PostApiClient", client =>
{
     client.BaseAddress = new Uri("https://localhost:7119/");


});
builder.Services.AddHttpClient("ProfileApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7273/");


});



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/authentication/login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAPIs",
        builder => builder
            .WithOrigins(
                "https://localhost:7082", // IdentityAPI
                "https://localhost:7119"  // PostAPI
                "https://localhost:7273/" //ProfileAPI
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//builder.WebHost.ConfigureKestrel(options =>
//{ options.ListenLocalhost(5000); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseCors("AllowAPIs");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
//app.MapControllerRoute(
//   name: "default",
//   pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
