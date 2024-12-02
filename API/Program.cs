using API.Middleware;
using Application.Interfaces;
using Application.Mediator_Handlers.Cafe.Commands;
using Application.Mediator_Handlers.Commands;
using Application.Mediator_Handlers.Employee.Commands;
using Application.Mediator_Handlers.Employee.Commands.CommandValidator;
using Application.Mediator_Handlers.Employee.Queries.QueryValidator;
using Application.Mediator_Handlers.Queries;
using Application.Profiles;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCafeCommand>();
builder.Services.AddMediatR(typeof(GetCafesQuery).Assembly);
builder.Services.AddMediatR(typeof(GetEmployeeByIdQuery).Assembly);
builder.Services.AddMediatR(typeof(CreateCafeCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateCafeCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteCafeCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(CreateEmployeeCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateEmployeeCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteEmployeeCommandHandler).Assembly);
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile<EmployeeMappingProfile>();
    mc.AddProfile<CafeMappingProfile>();
    mc.AddProfile<EmployeeCafeMappingProfile>();
});

mapperConfig.AssertConfigurationIsValid();
builder.Services.AddAutoMapper(typeof(CafeMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(EmployeeCafeMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(EmployeeMappingProfile).Assembly);



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("http://localhost:3000"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();

    
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedDataAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();
