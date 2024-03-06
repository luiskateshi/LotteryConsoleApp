using LotteryConsoleApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LotteryConsoleApp
{
    internal class Program
    {
        private readonly LotterySimulator _lotterySimulator;

        public Program(LotterySimulator lotterySimulator)
        {
            _lotterySimulator = lotterySimulator;
        }
        static void Main(string[] args)
        {

            // Create the host

            var host = CreateHostBuilder(args).Build();

            // Test database connection
            try
            {
                using (var serviceScope = host.Services.CreateScope())
                {
                    var serviceProvider = serviceScope.ServiceProvider;
                    var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
                    if(!dbContext.Database.CanConnect())
                    {
                        throw new Exception();
                    }
                    dbContext.Database.Migrate();
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Error connecting to the database: Check if the connection string is correct.");
                return;
            }

            // Start the program
            var lotterySimulator = host.Services.GetRequiredService<LotterySimulator>();
            lotterySimulator.menu();
        }
        public static void ConfigureServices(IServiceCollection services)
        {
            //Adding services inside the service collection using dependency injection
            services.AddDbContext<AppDbContext>();

            services.AddTransient<LotterySimulator>();
        }

        //configures the services that are declared above
        public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
           .ConfigureServices((_, services) =>
           {
               ConfigureServices(services);
           });
    


}
}