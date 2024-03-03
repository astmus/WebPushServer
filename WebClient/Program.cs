using FirebaseAdmin;
using LinqToDB.Extensions;
using LinqToDB.AspNet;
using Google.Apis.Auth.OAuth2;

using WebClient.Components;
using DataModels;
using LinqToDB;

namespace WebClient
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();

			builder.Services.AddSingleton<FirebaseApp>(sp =>
				FirebaseApp.Create(new AppOptions()
				{
					Credential = GoogleCredential.FromFile("profpushsender-firebase.json")
				}));

			builder.Services.AddLinqToDBContext<ILogsDB, LogsDB>((sp, opt) =>
			{
				return opt.UsePostgreSQL("Host=localhost;Database=logs;Username=postgres;Password=123;Persist Security Info=True");
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseAntiforgery();

			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();

			app.Run();
		}
	}
}
