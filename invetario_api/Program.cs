using invetario_api.database;
using invetario_api.Filters;
using invetario_api.Jwt;
using invetario_api.Modules.categories;
using invetario_api.Modules.unit;
using invetario_api.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using invetario_api.Modules.auth;
using invetario_api.Modules.users;
using invetario_api.Modules.store;
using invetario_api.Modules.products;
using invetario_api.Modules.provider;
using invetario_api.Modules.client;
using invetario_api.Modules.entryorder;
using invetario_api.Modules.images;
using Microsoft.Extensions.FileProviders;
using invetario_api.Modules.departureorder;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
    opt.Filters.Add<ApiFilter>();
    opt.Filters.Add<ValidateModelFilter>();
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<Database>(opt =>
{
    opt.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<IClientService, ClientService>();




builder.Services.AddScoped<IEntryorderService, EntryorderService>();

builder.Services.AddScoped<IImagesService, ImagesService>();

builder.Services.AddScoped<IDepartureorderService, DepartureorderService>();

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.InvalidModelStateResponseFactory = ctx =>
    {
        var errors = ctx.ModelState.Where(e => e.Value!.Errors.Count > 0).ToDictionary(
                e => e.Key,
                e => e.Value!.Errors.Select(x => x.ErrorMessage)
        );

        var res = ResponseApi<object>.ErrorModel(errors);

        return new BadRequestObjectResult(res);
    };
});


builder.Services.AddSingleton<JwtUtils>();


builder.Services.AddAuthentication(
    opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]!
            )
        )
    };

    opt.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var res = ResponseApi<object>.Unauthorized().toJson();
            await context.Response.WriteAsync(res);
        },

        OnForbidden = async context =>
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";

            var res = ResponseApi<object>.Forbidden().toJson();
            await context.Response.WriteAsync(res);
        }
    };
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "static")
    ),
    RequestPath = "/api/static"
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
