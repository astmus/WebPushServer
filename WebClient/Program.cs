using FirebaseAdmin;

using Google.Apis.Auth.OAuth2;

using WebClient.Components;

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
