
using BookManagemntApp.Models;
using BookManagemntApp.Repository;
using BookManagemntApp.Service;
using BookManagemntApp.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;



namespace BookManagemntApp
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DI Pattern
            // Register IBookRepository and IBookService with dependency injection
            
            //Register BookValidator with dependency injection
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            // Register the custom exception handling middleware
           
            app.MapControllers();
            
            

            app.Run();
        }
    }
}
