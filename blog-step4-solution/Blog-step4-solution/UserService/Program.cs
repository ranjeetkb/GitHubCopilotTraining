using UserService.Models;
using UserService.Repository;


namespace UserService
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

            //Add the UserDbSettings to the controller           

            builder.Services.Configure<UserDbSettings>(builder.Configuration.GetSection("MongoDB"));

            //Add the userdbsettings,repository and service to the container
            builder.Services.AddSingleton<UserDbSettings>();
            builder.Services.AddSingleton<IUserRepository,UserRepository>();
            builder.Services.AddSingleton<Service.IUserService,Service.UserService>();


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