using System.Text;
using System.Text.Json;

using OriApps.UniCommand.PlatformService.Data.DTO;

using RabbitMQ.Client;

namespace OriApps.UniCommand.PlatformService.Services;

public class MessageBusClient : IMessageBusClient
{
	private readonly IConnection _connection;
	private readonly IModel _channel;

	public MessageBusClient(IConfiguration configuration)
	{
		var factory = new ConnectionFactory 
		{
			HostName = configuration["RabbitMQ:HostName"],
			Port = int.Parse(configuration["RabbitMQ:Port"]!),
		};

		try
		{
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			
			_channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
			
			_connection.ConnectionShutdown += OnConnectionShutdown;
			
			Console.WriteLine("Connected to RabbitMQ");
		} 
		catch (Exception e)
		{
			Console.WriteLine($"Error while connecting to RabbitMQ: {e}");
		}
	}

	private void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
	{
		Console.WriteLine($"Connection to RabbitMQ closed: {e}");
	}

	public void PublishNewPlatform(PlatformPublishedDTO platformPublishedDTO)
	{
		var message = JsonSerializer.Serialize(platformPublishedDTO);

		if (!_connection.IsOpen)
		{
			Console.WriteLine("Connection to RabbitMQ is closed, can't publish");
		}
		
		Console.WriteLine($"Publishing message: {message}");
		
		SendMessage(message);
	}

	private void SendMessage(string message)
	{
		var body = Encoding.UTF8.GetBytes(message);
		
		_channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
		
		Console.WriteLine($"Message published {message}");
	}

	void IDisposable.Dispose()
	{
		if (_channel.IsOpen)
		{
			_connection.Close();
			_channel.Close();
		}
		
		_connection.Dispose();
		_channel.Dispose();
	}
}
