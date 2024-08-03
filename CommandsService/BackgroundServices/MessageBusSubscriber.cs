using System.Text;

using OriApps.UniCommand.CommandsService.EventProcessing;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OriApps.UniCommand.CommandsService.BackgroundServices;

public class MessageBusSubscriber : BackgroundService
{
	private readonly IEventProcessor _eventProcessor;
	private readonly IConfiguration _configuration;

	private IConnection _connection = null!;
	private IModel _channel = null!;
	private string _queueName = null!;
	private EventingBasicConsumer _consumer;

	public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
	{
		_eventProcessor = eventProcessor;
		_configuration = configuration;
		InitializeRabbitMQ();
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();
		
		_consumer = new EventingBasicConsumer(_channel);
		
		_channel.BasicConsume(queue: _queueName, autoAck: true, consumer: _consumer);

		_consumer.Received += OnMessageReceived;
		
		return Task.CompletedTask;
	}

	private void InitializeRabbitMQ()
	{
		var factory = new ConnectionFactory {
			HostName = _configuration["RabbitMQ:HostName"],
			Port = int.Parse(_configuration["RabbitMQ:Port"]!)
		};
		
		_connection = factory.CreateConnection();
		
		_channel = _connection.CreateModel();
		
		_channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
		_queueName = _channel.QueueDeclare().QueueName;
		_channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");
		
		Console.WriteLine("MessageBusSubscriber initialized");
		
		_connection.ConnectionShutdown += OnConnectionShutdown;
	}
	
	private void OnMessageReceived(object? sender, BasicDeliverEventArgs e)
	{
		Console.WriteLine($"Event received: {e.RoutingKey}");

		var body = e.Body;
		var notificationMessage = Encoding.UTF8.GetString(body.ToArray());
		
		_eventProcessor.ProcessEventAsync(notificationMessage);
		
	}

	public override void Dispose()
	{
		if (_channel.IsOpen)
		{
			_channel.Close();
			_connection.Close();
		}
		
		_channel?.Dispose();
		_connection?.Dispose();

		_consumer.Received -= OnMessageReceived;
		
		base.Dispose();
	}
	
	private void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
	{
		Console.WriteLine($"ConnectionShutdown: {e.ReplyText}");
	}
}