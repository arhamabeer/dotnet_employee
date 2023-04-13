//using dotnet_mvc.Models;

using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// DB CONNECTION
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConnection")));
//builder.Services.AddMvc();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddScoped<IEmployee_Repository, SqlEmployeeRepository>(); // SQL
//builder.Services.AddSingleton<IEmployee_Repository, MockEmployeeRepository>();  // in-memory(local memory)

var app = builder.Build();

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
