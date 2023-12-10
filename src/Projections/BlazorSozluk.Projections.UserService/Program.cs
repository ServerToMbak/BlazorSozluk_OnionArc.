using BlazorSozluk.Projections.UserService;
using BlazorSozluk.Projections.UserService.Services;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<EmailService>();
var host = builder.Build();
host.Run();
