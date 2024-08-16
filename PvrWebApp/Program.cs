using Microsoft.EntityFrameworkCore;
using PvrWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();


app.MapControllers();

app.Run();
