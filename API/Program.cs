
using API.Middleware;
using Core.Identity;
using Core.Interfaces;
using Infrastuctre;
using Infrastuctre.Data;
using Infrastuctre.Identity;
using Infrastuctre.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.AddIdentityCore<AppUser>(options =>
{

}).AddEntityFrameworkStores<AppIdentityDbContext>()
  .AddSignInManager<SignInManager<AppUser>>();

var jwt = builder.Configuration.GetSection("Token");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"])),
            ValidIssuer = jwt["Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();


builder.Services.AddScoped<IProductRepo,ProductRepo>();
builder.Services.AddScoped<IBasketRepo, BasketRepo>();
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
  {
      var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
      options.PreserveAsyncOrder = false;
      return ConnectionMultiplexer.Connect(options);
  });

//add swager autherize 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Next Driven API", Version = "v1" });
    c.ResolveConflictingActions(x => x.First());
    // Swagger 2.+ support
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                //Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new string[] {}
        }
    });
});

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
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCors(txt);
app.MapControllers();

using var scope = app.Services.CreateScope();
var Services = scope.ServiceProvider;
//create and update database 
var context = Services.GetRequiredService<StoreContext>();
var identitycontext = Services.GetRequiredService<AppIdentityDbContext>();
var usermanager = Services.GetRequiredService<UserManager<AppUser>>();
var logger = Services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await identitycontext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    await AppIdentityDbContextSeed.SeedUserAsync(usermanager);
}
catch (Exception ex)
{
    logger.LogError(ex, "error occured while migrations");
}

app.Run();
