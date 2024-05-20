using CommentService.API;
using CommentService.Application;
using CommentService.Infrastructure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Make sure to configure this if EVER goes into prod...
app.UseCors(options =>
{
    options.WithOrigins("*");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});


app.UseHttpsRedirection();

var imagesStorageConnectionString = builder.Configuration.GetSection("ConnectionStrings:ImageStorage").Value;
var imagesStoragePath = Path.Combine(Directory.GetCurrentDirectory(), imagesStorageConnectionString);


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesStoragePath),
    RequestPath = $"/{imagesStorageConnectionString}"
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
