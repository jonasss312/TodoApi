global using todo_api.Data;
global using Microsoft.EntityFrameworkCore;
using todo_api.Data.Repos;
using todo_api.Usecases.Interfaces;
using todo_api.Usecases.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using todo_api.Data.Interfaces;
using todo_api.Data.Implementations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
AddConnections(builder);
AddRepos(builder);
AddUsecases(builder);
AddGateways(builder);
AddAuthentication(builder);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options=>
{
    AllowTokenInHeaderForSwagger(options);
});
builder.Services.AddCors();


var app = builder.Build();

SeedData(app);

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

void AllowTokenInHeaderForSwagger(SwaggerGenOptions options)
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
}

void AddAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("SecretKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

void AddConnections(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<Context>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

void AddRepos(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<AssignmentRepo>();
    builder.Services.AddTransient<UserRepo>();
}

void AddUsecases(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<GetAllUserAssignmentsUC, GetAllUserAssignmentsInteractor>();
    builder.Services.AddTransient<GetUserAssignmentUC, GetUserAssignmentInteractor>();
    builder.Services.AddTransient<CreateAssignmentUC, CreateAssignmentInteractor>();
    builder.Services.AddTransient<DeleteAssignmentUC, DeleteAssignmentInteractor>();
    builder.Services.AddTransient<UpdateAssignmentUC, UpdateAssignmentInteractor>();
    builder.Services.AddTransient<CreateNewUserUC, CreateNewUserInteractor>();
    builder.Services.AddTransient<GetUserByEmailUC, GetUserInteractor>();
    builder.Services.AddTransient<CreateTokenUC, CreateTokenInteractor>();
    builder.Services.AddTransient<VerifyUserPasswordUC, VerifyUserPasswordInteractor>();
    builder.Services.AddTransient<VerifyAssignmentCreatorOrAdminUC, VerifyAssignmentCreatorOrAdminInteractor>();
    builder.Services.AddTransient<VerifyUserIdOrAdminUC, VerifyUserIdOrAdminInteractor>();
    builder.Services.AddTransient<SendRecoveryEmailUC, SendRecoveryEmailInteractor>();
    builder.Services.AddTransient<CreatePasswordHashUC, CreatePasswordHashInteractor>();
    builder.Services.AddTransient<UpdateUserPasswordUC, UpdateUserInteractor>();
    builder.Services.AddTransient<GetUserByTokenUC, GetUserInteractor>();
}

void AddGateways(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<MailGW, MailSender>();
    builder.Services.AddTransient<DatabaseSeeder>();
}

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DatabaseSeeder>();
        service.Seed();
    }
}