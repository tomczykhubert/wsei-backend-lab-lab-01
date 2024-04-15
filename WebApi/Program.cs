using ApplicationCore.Interfaces.AdminService;
using ApplicationCore.Interfaces.Repository;
using BackendLab01;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Memory;
using Infrastructure.Memory.Repository;
using Infrastructure.Services;
using WebApi.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IQuizUserService, QuizUserServiceEF>();
//builder.Services.AddSingleton<IQuizAdminService, QuizAdminService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<QuizItem>, QuizItemValidator>();
builder.Services.AddDbContext<QuizDbContext>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
