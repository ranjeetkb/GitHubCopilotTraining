using CommentService.Models;
using CommentService.Repository;

namespace CommentService
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

            //Add the CommentDbSettings to the controller           
            builder.Services.Configure<CommentDbSettings>(builder.Configuration.GetSection("MongoDB"));
            

            //Add the userdbsettings,repository and service to the container
            builder.Services.AddSingleton<CommentDbSettings>();
            builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
            builder.Services.AddSingleton<Service.ICommentService, Service.CommentService>();

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