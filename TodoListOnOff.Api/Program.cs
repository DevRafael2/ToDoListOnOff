using System.Text;
using Microsoft.IdentityModel.Tokens;
using ToDoListOnOff.Application.UseCases;
using ToDoListOnOff.Infraestructure.Persistence;
using ToDoListOnOff.Application.Mappers;
using ToDoListOnOff.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services
    .AddPersistence(builder.Configuration)
    .AddUseCases()
    .AddValidators()
    .AddMappers();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateActor = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["KeyTokenSecurity"]!)),
        ClockSkew = TimeSpan.Zero,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
