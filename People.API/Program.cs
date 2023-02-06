using People.API;
using People.API.Endpoints.People;
using People.API.Localizer;
using People.API.Seeding;
using People.API.Swagger;
using People.Application;
using People.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPeopleAPIServices();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHostedService<SeedingHostedService>();
}

builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AddRequiredHeaderParameters>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(exHandlerBuilderApp =>
{
    exHandlerBuilderApp.Run(async context =>
        await Results.Problem().ExecuteAsync(context));
});

app.RegisterLocale();

app.UseHttpsRedirection();

app.RegisterPeopleEndpoints();

app.Run();
