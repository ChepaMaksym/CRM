using CRM.Services;
using CRM.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("DefaultConnection")
                       ?? throw new ArgumentNullException("Connection string 'DefaultConnection' is missing.");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISoilTypeService>(provider =>
    new SoilTypeService(connectionString));

builder.Services.AddTransient<IGardenService>(provider =>
    new GardenService(connectionString));

builder.Services.AddTransient<IPlotService>(provider =>
    new PlotService(connectionString));

builder.Services.AddTransient<IPotService>(provider =>
    new PotService(connectionString));

builder.Services.AddTransient<IPlantService>(provider =>
    new PlantService(connectionString));

builder.Services.AddTransient<IWateringService>(provider =>
    new WateringService(connectionString));

builder.Services.AddTransient<IHarvestService>(provider =>
    new HarvestService(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
