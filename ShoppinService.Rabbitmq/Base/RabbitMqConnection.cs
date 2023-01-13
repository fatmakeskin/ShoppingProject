using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqService.Base
{
    public class RabbitMqConnection
    {
        public RabbitMqConnection()
        {
            GetConnection();
        }
        public IConnection Connection { get; set; }
        public bool IsConnected { get; set; } = false;

        private static readonly object _lockObj = new object();


        public IConnection GetConnection()
        {
            if (IsConnected)
                return Connection;
            try
            {
                lock (_lockObj)
                {
                    if (IsConnected)
                        return Connection;
                    ConnectionFactory connectionFactory = new ConnectionFactory
                    {
                        Uri = new Uri(Environment.GetEnvironmentVariable("RABBITMQ_URI"))
                    };
                    Connection = connectionFactory.CreateConnection();
                    IsConnected = true;
                    return Connection;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IModel GetChannel(string queue)
        {
            GetConnection();
            IModel channel = Connection.CreateModel();
            channel.QueueDeclare(queue);
            return channel;
        }
    }
}
