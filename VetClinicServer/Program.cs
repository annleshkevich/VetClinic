using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VetClinicServer;
using VetClinicServer.BusinessLogic.Implementations;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Enums;
using VetClinicServer.Model.Context;
using VetClinicServer.Model.Models;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VetClinicContext>(options => options.UseSqlServer(connection));
var optionsBuilder = new DbContextOptionsBuilder<VetClinicContext>();
var options = optionsBuilder.UseSqlServer(connection).Options;
builder.Services.AddControllers();

//using (VetClinicContext db = new(options))
//{
//    User user = new()
//    {
//        Login = "",
//        Email = ""

//    };
//    Animal animal1 = new()
//    {
//        Name = "Soa",
//        Age = 4,
//        Breed = "Poodle",
//        Img = "https://kisapes.ru/wp-content/uploads/2021/05/toy-poodle.jpg",
//        User = user
//    };
//    Animal animal2 = new()
//    {
//        Name = "Poo",
//        Age = 7,
//        Breed = "Poodle",
//        Img = "https://www.purina.ru/sites/default/files/styles/nppe_breed_selector_500/public/2020-04/poodle_toy.jpg?itok=3auArWUi",
//        User = user
//    };
//    db.Animals.AddRange(animal1, animal2);

//    Appointment appointment1 = new()
//    {
//        BehavioralNote = "jhjkbj",
//        Complaint = "nnlkm",
//        CreatedDate = DateTime.Now,
//        Animal = animal2
//    };
//    Appointment appointment2 = new()
//    {
//        BehavioralNote = "jhjkbj",
//        Complaint = "nnlkm",
//        CreatedDate = DateTime.Now,
//        Animal = animal1
//    };
//    db.Appointments.AddRange(appointment1, appointment2);

//    db.SaveChanges();
//}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Api",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
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

builder.Services.AddTransient<IAnimalService, AnimalService>();
builder.Services.AddTransient<IAppointmentService, AppointmentService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IHashService, HashService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

