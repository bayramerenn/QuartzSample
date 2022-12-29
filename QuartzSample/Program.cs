using CrystalQuartz.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Quartz;
using QuartzSample.Helper;
using QuartzSample.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuartz(config =>
{
    config.UseMicrosoftDependencyInjectionJobFactory();
    config.AddQuartzCustom(builder.Configuration,new PrintMessageJob());
    config.AddQuartzCustom(builder.Configuration,new DenemeJob());
});
    

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(httpClient =>
{
    httpClient.BaseAddress = new Uri("")
}).AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(
            3, retryNumber => TimeSpan.FromMilliseconds(600)));

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