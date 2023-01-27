global using Explorations.Models;
using Explorations.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<PlantsService>();

var mvcBuilder = builder.Services.AddRazorPages(o =>
{
    // This is to make demos easier, don't do this in production
    o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
});

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();