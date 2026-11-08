using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Ona_Mihai_Lab2.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Ona_Mihai_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Ona_Mihai_Lab2Context' not found.");

builder.Services.AddDbContext<Ona_Mihai_Lab2Context>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<LibraryIdentityContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<LibraryIdentityContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
