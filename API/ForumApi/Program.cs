using System.Text.Json.Serialization;
using ForumApi.Data.Repository;
using ForumApi.Extensions;
using ForumApi.Middlewares;
using ForumApi.Options;
using ForumApi.Services;
using ForumApi.Services.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Services.AddAppOptions(builder.Configuration);
builder.Services.AddRepository(builder.Configuration);

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ISectionService, SectionService>();
builder.Services.AddScoped<IForumService, ForumService>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IBanService, BanService>();
builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddControllers()
  .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "QuakeAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Description = @"Example: 'Bearer eyASDsddw....'",
         Name = "Authorization",
         In = ParameterLocation.Header,
         Type = SecuritySchemeType.ApiKey,
         Scheme = "Bearer"
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
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
      });
});

builder.Services.ConfigureAutoMapper();
builder.Services.AddValidators();
builder.Services.AddJwtAuth(builder.Configuration);

var frontCorsPolicy = "frontCorsPolicy";
builder.Services.AddCors(options => 
{
  options.AddPolicy(
    name : frontCorsPolicy, 
    policy => 
    {
      policy.WithOrigins("http://localhost:4200")
            .WithOrigins("https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(frontCorsPolicy);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI( options => {
  options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
  options.RoutePrefix = "";
});

app.MapControllers();

app.Run("http://0.0.0.0:5000");
