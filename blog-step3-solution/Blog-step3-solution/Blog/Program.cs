using Entities;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Blog
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

            //Add the database context to the controller
            builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbContext")));

            //Add the repository to the container
            builder.Services.AddScoped<IRepository<BlogPost>,BlogPostRepository>();
            builder.Services.AddScoped<IRepository<Category>, CategoryRepository >();
            builder.Services.AddScoped<IRepository<User>,UserRepository>();

            builder.Services.AddScoped<TagRepository>();
            builder.Services.AddScoped<CommentRepository>();
            builder.Services.AddScoped<LikeRepository>();


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