global using todo_api.Data;
global using Microsoft.EntityFrameworkCore;
using todo_api.Data.Repos;
using todo_api.Usecases.Interfaces;
using todo_api.Usecases.Implementations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<AssignmentRepo>();
builder.Services.AddTransient<UserRepo>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddUsecases(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

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
}
