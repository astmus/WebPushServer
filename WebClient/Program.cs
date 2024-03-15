using FirebaseAdmin;
using LinqToDB.Extensions;
using LinqToDB.AspNet;
using Google.Apis.Auth.OAuth2;

using WebClient.Components;
using DataModels;
using LinqToDB;
using Google.Cloud.PubSub.V1;
using Grpc.Core;
using Google.Protobuf.Collections;
using System.Text;
using Google.Protobuf;

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

	public static class ClientExtensions
	{
		public static Topic NewTopic(this PublisherServiceApiClient publisher, string projectId, string topicId)
		{
			var topicName = TopicName.FromProjectTopic(projectId, topicId);
			Topic topic = null!;

			try
			{
				topic = publisher.CreateTopic(topicName);
				Console.WriteLine($"Topic {topic.Name} created.");
			}
			catch (RpcException e) when (e.Status.StatusCode == StatusCode.AlreadyExists)
			{
				Console.WriteLine($"Topic {topicName} already exists.");
			}
			return topic;
		}

		public static IEnumerable<string> ListSubscriptionsInTopic(this PublisherServiceApiClient publisher, string projectId, string topicId)
		{
			TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
			IEnumerable<string> subscriptions = publisher.ListTopicSubscriptions(topicName);
			return subscriptions;
		}

		public static async Task<PublishResponse?> PublishMessagesAsync(this PublisherServiceApiClient publisher, string projectId, string topicId, IEnumerable<string> messageTexts)
		{
			TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);

			int publishedMessageCount = 0;
			var publishTasks = messageTexts.Select(text =>
			{
				try
				{
					return new PubsubMessage
					{

						Data = ByteString.CopyFromUtf8(text),
						Attributes =
						{
							{ "description", "Simple text message" }
						}
					};
				}
				catch (Exception exception)
				{
					Console.WriteLine($"An error occurred when publishing message {text}: {exception.Message}");
				}
				return default;
			}).ToList();
			try
			{
				return await publisher.PublishAsync(topicName, publishTasks);
			}
			catch (Exception err)
			{
				Console.WriteLine($"Published message {err.Message}");
			}

			return default;
		}
	}
}
