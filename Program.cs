//using dotnet_mvc.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// DB CONNECTION
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        );
                    }));
builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase  = false;
        options.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<AppDbContext>();
//builder.Services.AddMvc();
builder.Services.AddMvc(options => {
    // USE AUTHORIZATION POLICY TO ALLOW ONLY AUTHORIZE USER TO USE APP
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
    // USE AUTHORIZATION POLICY TO ALLOW ONLY AUTHORIZE USER TO USE APP (END)
    options.EnableEndpointRouting = false;
    });
builder.Services.AddScoped<IEmployee_Repository, SqlEmployeeRepository>(); // SQL
//builder.Services.AddSingleton<IEmployee_Repository, MockEmployeeRepository>();  // in-memory(local memory)



// Configure NLog
builder.Logging.ClearProviders();
builder.Logging.AddNLog();

var app = builder.Build();


// LOGGER 

var path = builder.Configuration["Logging:Configuration:Path"];
if (!Directory.Exists(path))
    Directory.CreateDirectory(path);

var fileName = builder.Configuration["Logging:Configuration:FileName"];

// Below code 'loggerFactory' has been commented for testing purpose
//builder.Logging.AddProvider(new NReco.Logging.File.FileLoggerProvider(fileName, true)
//{
//    FormatLogEntry = (msg) =>
//    {
//        var sb = new System.Text.StringBuilder();
//        StringWriter sw = new StringWriter(sb);
//        var jsonWriter = new Newtonsoft.Json.JsonTextWriter(sw);
//        jsonWriter.WriteStartArray();
//        jsonWriter.WriteValue(DateTime.Now.ToString("o"));
//        jsonWriter.WriteValue(msg.LogLevel.ToString());
//        jsonWriter.WriteValue(msg.EventId.Id);
//        jsonWriter.WriteValue(msg.Message);
//        jsonWriter.WriteValue(msg.Exception?.ToString());
//        jsonWriter.WriteEndArray();
//        return sb.ToString();
//    }
//});



// LOGGER

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    Console.WriteLine("Error IF");

   
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    Console.WriteLine("Error ELSE");
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

// MVC SERVICE

//app.UseMvcWithDefaultRoute();
app.UseMvc(route =>
{
    route.MapRoute(
        "default",
        "{controller=Home}/{action=AllEmployees}/{id?}"
    );
});

app.UseRouting();



app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=AllEmployees}/{id?}");

app.Run();

