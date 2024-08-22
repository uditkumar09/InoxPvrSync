using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PvrWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddSwaggerGen(
    c =>
{
c.SwaggerDoc("v1", new OpenApiInfo { Title = "v1", Version = "v1", Description = "Swagger Users API" });
//c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
//c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
//{
//    Description = @"Authorization header using the Bearer scheme. \r\n\r\n 
//        Enter 'Bearer' [space] and then your token in the text input below.
//        \r\n\r\nExample: 'bearer 12345abcdef'",
//    Name = "Authorization",
//    In = ParameterLocation.Header,
//    Type = SecuritySchemeType.ApiKey,
//    Scheme = "bearer"
//});

}
);


builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Configure the HTTP request pipeline.
var app = builder.Build();
//app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        config =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    }
    );
}
else
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();
//app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();


app.MapControllers();

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync($"Request received at{context.Request.Path}");
//});
app.Run();
