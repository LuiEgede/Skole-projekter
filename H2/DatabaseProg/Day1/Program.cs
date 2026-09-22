using Day1.Data;
using Day1.Services;

using var context = new AppDbContext();

context.Database.EnsureCreated();
DbSeeder.Seed(context);
