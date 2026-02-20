using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Project.Storage;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var connectionString = config.GetConnectionString("Connection")!;

var context = new ProjectDbContext(connectionString);
await context.Database.MigrateAsync();

IStorage storage = new Storage();

await storage.Save("Привет, ");
await storage.Save("Мир!");

var hello = await storage.Retrieve(1);
var world = await storage.Retrieve(2);