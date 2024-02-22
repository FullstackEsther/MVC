using BloggieMvc.Context;
using BloggieMvc.Repositories.Implementation;
using BloggieMvc.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BloggieContext>(opt => opt.UseMySQL(builder.Configuration.GetConnectionString("BloggieString")));

builder.Services.AddScoped<ITagRepository,TagRepository>();
builder.Services.AddScoped<IBlogPostRepository,BlogPostRepository>();
builder.Services.AddScoped<IImageRepository,CloudinaryImageRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
