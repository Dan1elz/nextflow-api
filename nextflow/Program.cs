using System.Security.Claims; // <<-- MUDANÇA 1: Adicionar este using
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using nextflow.Application.UseCases.Users;
using nextflow.Application.Utils;
using nextflow.Domain.Interfaces.Utils;
using nextflow.Infrastructure.Database;
using nextflow.Infrastructure.Repositories;
using nextflow.Middlewares;

namespace nextflow;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // *** CONFIGURAÇÃO DO DATABASE CONTEXT ***
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // *** CONFIGURAÇÃO DE CORS ***
        builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
        {
            policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }));

        // *** CONFIGURAÇÃO DO SWAGGER ***
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        // *** CONFIGURAÇÃO DE AUTENTICAÇÃO JWT ***
        var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = ClaimTypes.Role
            };
        });
        builder.Services.AddAuthorization();

        builder.Services.Configure<JwtUtils.JwtSettingsUseCase>(
            builder.Configuration.GetSection("JwtSettings"));

        // *** ADICIONANDO CONTROLLERS COM SUPORTE A NEWTONSOFT.JSON ***
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        builder.Services.AddEndpointsApiExplorer();

        // *** REGISTRO DE DEPENDÊNCIAS (SCRUTOR) ***
        builder.Services.Scan(scan => scan
            .FromAssemblyOf<CreateUserUseCase>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("UseCase")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        builder.Services.Scan(scan => scan
            .FromAssemblyOf<UserRepository>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        builder.Services.AddScoped<IStorageService, LocalStorageService>();
        builder.Services.AddScoped<JwtUtils>();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // *** CONFIGURAÇÃO DO APP ***
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Executa Financeiro API V2");
                c.RoutePrefix = string.Empty;
            });
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCors();
        app.UseRouting();

        // *** MIDDLEWARES ***
        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        // *** MAPEAMENTO DE CONTROLLERS ***
        app.MapControllers();

        // *** EXECUÇÃO DA APLICAÇÃO ***
        app.Run();
    }
}