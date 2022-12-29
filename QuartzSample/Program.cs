using CrystalQuartz.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Polly;
using Quartz;
using QuartzSample.Helper;
using QuartzSample.Servives.Concerete;
using QuartzSample.Tasks;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddQuartz(config =>
{
    var quartzSettings = builder.Configuration.GetSection(nameof(QuartzSettings)).Get<List<QuartzSettings>>();

    config.UseMicrosoftDependencyInjectionJobFactory();
    config.AddQuartzCustom<PrintMessageJob>(quartzSettings);
    config.AddQuartzCustom<DenemeJob>(quartzSettings);
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddHttpClient<IPropertyOwnerService,PropertyOwnerService>(c =>
{
    c.BaseAddress = new Uri("https://localhost:7237/");

}).SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(5, retryNumber => TimeSpan.FromSeconds(10))) // 10 sn arayla 5 kere deneme yapar
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(50))); // 50 sn icinde 5 kez hata olursa baglantiyi kes


builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var schedulerFactory = scope.ServiceProvider.GetRequiredService<ISchedulerFactory>();
var scheduler = schedulerFactory
    .GetScheduler()
    .GetAwaiter()
    .GetResult();

app.UseCrystalQuartz(() => scheduler);

app.Run();