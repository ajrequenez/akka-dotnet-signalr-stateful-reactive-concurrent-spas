using Akka.DependencyInjection;
using Akka.Hosting;
using Game.ActorModel.Actors;
using Game.Web.Hubs;
using System.Diagnostics;

namespace Game.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<GameHubHelper, GameHubHelper>();

            builder.Services.AddAkka("GameSystem", configurationBuilder =>
            {
                configurationBuilder.WithActors((sys, registry) =>
                {
                    var actor = sys.ActorOf(EchoActor.Props(), "echoActor");
                    var gameController = sys.ActorOf(GameControllerActor.Props(), "gameController");

                    registry.Register<EchoActor>(actor);
                    registry.Register<GameControllerActor>(gameController);

                    var signalRProps = DependencyResolver.For(sys).Props<SignalRActor>(gameController);
                    var signalRActor = sys.ActorOf(signalRProps, "signalr");

                    registry.Register<SignalRActor>(signalRActor);
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<GameHub>("/gameHub");
            });
            

            app.Run();
        }
    }
}