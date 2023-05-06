
using API.Middleware;
using Core.Interfaces;
using Infrastuctre;
using Infrastuctre.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string txt = "hi";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepo,ProductRepo>();
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddCors(opt =>
//{
//    opt.AddPolicy("CorsPolicy", policy =>
//    {
//        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost/4200");
//    });
//});

  builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt, builder =>
                {
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                });
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("errors/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseCors(txt);
app.MapControllers();

using var scope = app.Services.CreateScope();
var Services = scope.ServiceProvider;
var context = Services.GetRequiredService<StoreContext>();
var logger = Services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "error occured while migrations");
}

app.Run();
