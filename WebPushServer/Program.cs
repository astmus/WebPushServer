using System.Net;

using CorePush.Firebase;

using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;

using Google.Apis.Auth.OAuth2;

using Newtonsoft.Json;

namespace WebPushServer
{
	public class Program
	{
		private static Dictionary<string, string> subscriptions = new Dictionary<string, string>();
		static FirebaseApp fireApp;
		static async Task Main(string[] args)
		{
			fireApp = FirebaseApp.Create(new AppOptions()
			{
				Credential = GoogleCredential.FromFile("profpushsender-firebase.json")
			});
			Console.WriteLine(fireApp.Name); // "[DEFAULT]"

			// Retrieve services by passing the defaultApp variable...
			var defaultAuth = FirebaseAuth.GetAuth(fireApp);

			var host = new WebHostBuilder()
				.UseKestrel()
				.Configure(app =>
				{
					app.UseRouting().UseStaticFiles();
					app.UseEndpoints(endpoints =>
					{
						endpoints.MapPost("/subscribe", async context =>
						{
							var subscriptionJson = await new StreamReader(context.Request.Body).ReadToEndAsync();
							// Здесь вы можете сохранить подписку в базу данных или другое хранилище
							string endpoint = "http://localhost:5063/"; // извлекаем endpoint из subscriptionJson
							string clientId = "138500"; // извлекаем client id из subscriptionJson
							subscriptions[clientId] = endpoint;
							Console.WriteLine("Received subscription: " + subscriptionJson);
							context.Response.StatusCode = (int)HttpStatusCode.OK;
						});

						endpoints.MapPost("/send-notification", async context =>
						{
							var notificationJson = await new StreamReader(context.Request.Body).ReadToEndAsync();
							// Здесь вы можете отправить уведомление на подписку

							// Отправляем уведомление каждому подписчику
							await SendNotification("subscription.Value", notificationJson);

							Console.WriteLine("Received notification: " + notificationJson);
							context.Response.StatusCode = (int)HttpStatusCode.OK;
						});

						endpoints.MapPost("/send-now", async context =>
						{
							var tickDate = DateTime.Now.Date.Ticks.ToString();
							var condition = $"'{tickDate}' in topics";
							//var tokens = new List<string>() 
							//{ "ck2HpDXtTWOR33cVOSHh-r:APA91bG9cSoBQ2uNw3_7iFJzjLlUxyFHRkv-2gTVTQRL1gMBNvyUWTkoXlSJTUO5arA1VczauFfrthgHnnbrX1BO_vC4Oy_hFhJp3y7Xe0PHctlcVf9uwmYMByLhjuRjpV6EgizwqDTX" };
							//var response = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(
							//		tokens, tickDate);

							var message = new Message()
							{
								Notification = new Notification() { Title = $"Channel of  {tickDate}", Body = $"This is message of {tickDate}" },
								//Data = new Dictionary<string, string>()
								//{
								//	["notification"] = JsonConvert.SerializeObject(new { title = "title", body = "body" })
								//},
								Condition = condition//,
													 //Token = "ck2HpDXtTWOR33cVOSHh-r:APA91bG9cSoBQ2uNw3_7iFJzjLlUxyFHRkv-2gTVTQRL1gMBNvyUWTkoXlSJTUO5arA1VczauFfrthgHnnbrX1BO_vC4Oy_hFhJp3y7Xe0PHctlcVf9uwmYMByLhjuRjpV6EgizwqDTX"
													 //Token = "edxSYCKT50WwkvkYjSkNle:APA91bElkUBf5JyMcJbmZbyJZbqHD1crw-O741ws1LWAGxuvtQW2TOuB9fPqG8FD4FhdD8TdQUzj-Jmq1EvhQt66_8ZHqQM3Ic2LuYuf3jeiMYYMln7r7Eno2wGhD_fvuLw4dEcXiLYB"
							};

							var messaging = FirebaseMessaging.GetMessaging(fireApp);

							var response = await messaging.SendAsync(message);
							Console.WriteLine("Successfully sent message: " + response);
						});
					});
				});
			host.ConfigureServices(services =>
			{
				services.AddRouting();
			});

			await host.Build().RunAsync();
		}

		static async Task SendNotification(string endpoint, string notificationJson)
		{
			var message = new Message()
			{
				Notification = new Notification() { Title = "Notif title", Body = "Please click me" },
				//Data = new Dictionary<string, string>()
				//{
				//	["notification"] = JsonConvert.SerializeObject(new { title = "title", body = "body" })
				//},
				Token = "ck2HpDXtTWOR33cVOSHh-r:APA91bG9cSoBQ2uNw3_7iFJzjLlUxyFHRkv-2gTVTQRL1gMBNvyUWTkoXlSJTUO5arA1VczauFfrthgHnnbrX1BO_vC4Oy_hFhJp3y7Xe0PHctlcVf9uwmYMByLhjuRjpV6EgizwqDTX"
				//Token = "edxSYCKT50WwkvkYjSkNle:APA91bElkUBf5JyMcJbmZbyJZbqHD1crw-O741ws1LWAGxuvtQW2TOuB9fPqG8FD4FhdD8TdQUzj-Jmq1EvhQt66_8ZHqQM3Ic2LuYuf3jeiMYYMln7r7Eno2wGhD_fvuLw4dEcXiLYB"
			};

			var messaging = FirebaseMessaging.GetMessaging(fireApp);

			var response = await messaging.SendAsync(message);
			Console.WriteLine("Successfully sent message: " + response);
		}
	}
}
