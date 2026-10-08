var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientSidePolicy", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("ClientSidePolicy");

app.MapControllers();
app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();