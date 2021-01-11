using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Common;

namespace PersonService
{
    public class PersonReceiver : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        //private readonly IPersonReceiver _person_receicver;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private readonly ILogger _logger;

        public PersonReceiver(ILogger<PersonReceiver> logger)
        {
            //_hostname = rabbitMqOptions.Value.Hostname;
            _hostname = "localhost";
            //_queueName = rabbitMqOptions.Value.QueueName;
            _queueName = "person.queue";
            //_username = rabbitMqOptions.Value.UserName;
            _username = "guest";
            //_password = rabbitMqOptions.Value.Password;
            _password = "guest";
            _logger = logger;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            _logger.LogInformation("ExecuteAsync was executed!");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                IMessage personMessage = JsonConvert.DeserializeObject<IMessage>(content);

                HandleMessage(personMessage);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(IMessage Message)
        {
            //_person_receicver.Update(person);
            PersonMessage personMessage = (PersonMessage)Message;
            _logger.LogInformation(personMessage.Person.Nachname);
        }

        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation("OnConsumerCancelled was executed!");
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation("OnConsumerUnregistered was executed!");
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation("OnConsumerRegistered was executed!");
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation("OnConsumerShutdown was executed!");
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation("RabbitMQ_ConnectionShutdown was executed!");
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
