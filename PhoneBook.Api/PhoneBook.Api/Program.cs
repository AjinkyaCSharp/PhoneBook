using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using PhoneBook.Api.Authentication;
using PhoneBook.Database;
using PhoneBook.Middileware;
using PhoneBook.Repository;
using PhoneBook.Service;

namespace PhoneBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var corsPolicy = "allow-all-origins";
            var builder = WebApplication.CreateBuilder(args);
            //Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy, policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });

            //Enforce lower case url
            builder.Services.Configure<RouteOptions>(config =>
            {
                config.LowercaseUrls = true;
            });
            //Add logger
            builder.Logging.AddConsole();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                             new string[] {}
                     }
                 });
            });

            builder.Services.AddDbContext<PhoneBookDbContext>();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddScoped<IContactService, ContactService>();
            builder.Services.AddScoped<IConatactRepository, ContactRepository>();
            //builder.Services.AddScoped<ApiKeyAuthFilter>();

            builder.Services.AddSingleton<FirebaseAuthenticationHandler>();
            builder.Services.AddSingleton<FirebaseAuthenticationFunctionHandler>();



            var fireBaseApp = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("FirebaseConfig"))
            });

            builder.Services.AddSingleton(fireBaseApp);

            builder.Services
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, optionns => { });

            var app = builder.Build();

            app.UseAuthentication();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(corsPolicy);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}