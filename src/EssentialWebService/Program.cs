using System.Reflection;
using Microsoft.OpenApi.Models;
using Serilog;
using IdentityCore.Declarations.Repositories;
using IdentityCore.Declarations.Commands;
using BaseCore.Middlewares;
using BaseCore.Extensions;
using IdentityCore.Implementations.Commands.Validators;
using BaseCommon;
using BlobCore.Implementations.Commands.Validators;
using BlobCore.Declarations.Repositories;
using BlobCore.Declarations.Commands;
using EmailCore.Declarations.Commands;
using EmailCore.Declarations.Repositories;
using BaseCore.Services;
using EmailCore.Declarations.Services;
using EmailCore.Implementations.Services;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.Request;
});

#endif

builder.Services.AddCors(options =>
{

#if DEBUG
    options.AddPolicy("CorsPolicy", policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
#else
    options.AddPolicy("CorsPolicy", policy => { policy.WithOrigins().SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod(); });
#endif

});

// Add http context accessor
builder.Services.AddHttpContextAccessor();

// Add mediator
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AuthenticateCommand).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UploadBlobFileCommand).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateEmailTemplateCommand).GetTypeInfo().Assembly));

// Add validators
builder.Services.AddValidators<AuthenticateCommandValidator>();
builder.Services.AddValidators<UploadBlobFileCommandValidator>();
builder.Services.AddValidators<CreateEmailTemplateCommand>();

// Add services
builder.Services.AddCoreServices<IMongoDbService>();
builder.Services.AddCoreServices<IEmailSenderService>();

builder.Services.AddHostedService<EmailSenderHostedService>();

// Add repositories
builder.Services.AddRepositories<IAuthTokenRepository>();
builder.Services.AddRepositories<IBlobFileRepository>();
builder.Services.AddRepositories<IEmailTemplateRepository>();

builder.Services.AddMvc();

// Add services to the container.
builder.Services.AddControllers();

var environemntVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (environemntVariable != null && Constants.AllowedSwaggerEnvironments.Contains(environemntVariable))
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Essential Web Service", Version = "v1" });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{}
            }
        });
    });
}

// Add serilog
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

#if !DEBUG
builder.Logging.ClearProviders();
#endif

builder.Logging.AddSerilog(logger);

// App build
var app = builder.Build();

if (app.Environment.IsDevelopment() || Constants.AllowedSwaggerEnvironments.Contains(environemntVariable))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#if DEBUG
app.UseHttpLogging();
#endif

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseMiddleware<JwtMiddleware>(); // custom jwt auth middleware

app.MapControllers();

app.Run();