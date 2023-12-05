using BlazorSozluk.Api.Infrastructure.Persistence.Extensions;
using BlazorSozluk.Api.Application.Extensions;
using FluentValidation.AspNetCore;
using BlazorSozluk.Api.WebApi.Infrastructure.Extensions;
using BlazorSozluk.Api.WebApi.Infrastructure.ActionFilters;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => opt.Filters.Add<ValidationModelStateFilter>())
    .AddJsonOptions( opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddFluentValidation()
    .ConfigureApiBehaviorOptions(o =>o.SuppressModelStateInvalidFilter = true );


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

app.UseAuthentication();    
app.UseAuthorization();

app.UseCors("MyPolicy");
app.MapControllers();

app.Run();
