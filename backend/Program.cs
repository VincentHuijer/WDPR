using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string jsonString = File.ReadAllText("secrets.json");
Configuration configuration = JsonConvert.DeserializeObject<Configuration>(jsonString);
builder.Services.AddDbContext<GebruikerContext>(options =>{
    options.UseNpgsql(configuration.ConnectionString);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>{
     options.AddPolicy(name: "MyAllowedSpecificOrigins",
                       policy  =>
                       {
                           policy.AllowAnyOrigin()
                           .AllowAnyHeader().AllowAnyMethod(); // add the allowed origins
                       });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyAllowedSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
