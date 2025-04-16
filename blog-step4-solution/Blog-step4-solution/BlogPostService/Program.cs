using BlogPostService.Models;
using BlogPostService.Repository;
using BlogPostService.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BlogPostService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //Add the controllers to the container
            builder.Services.AddControllers();

            //Add the BlogDbSettings to the controller           

            builder.Services.Configure<BlogDbSettings>(builder.Configuration.GetSection("MongoDB"));

            //Add the userdbsettings,repository and service to the container
            builder.Services.AddSingleton<BlogDbSettings>();
            builder.Services.AddSingleton<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddSingleton<IBlogPostService, Service.BlogPostService>();

            builder.Services.AddSingleton<ISearchRepository, SearchRepository>();
            builder.Services.AddSingleton<ISearchService, Service.SearchService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();
            

            app.Run();
        }
    }
}